using DTO.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Common.DbContext
{
    public class MySqlConnection :  IDisposable
    {
        // added by priyam
        // modified by Gautam
        public string DbConnection;
        private IntPtr handle;  // Pointer to an external unmanaged resource.
        public MySql.Data.MySqlClient.MySqlConnection _MyConnection; // Other managed resource this class uses.
        private bool disposed = false;  // Track whether Dispose has been called.
        
        public MySqlConnection()
        {
            try
            {   _MyConnection = new MySql.Data.MySqlClient.MySqlConnection
                {
                    ConnectionString = DbConnection
                };
                _MyConnection.OpenAsync();
            }
            catch (MySql.Data.MySqlClient.MySqlException SqEx)
            {
                throw new Exception(Utilities.ErrorCodes.ProcessException(SqEx, "", "", "",
                    Utilities.ErrorCodes.MySqlExceptionMsg(SqEx)));
            }
        }
        
        public MySqlConnection(IConfiguration configuration)
        {
            try
            {
                this.DbConnection = configuration.GetConnectionString("DefaultConnection");

                _MyConnection = new MySql.Data.MySqlClient.MySqlConnection
                {
                    ConnectionString = configuration.GetConnectionString("DefaultConnection")
                };
                _MyConnection.OpenAsync();
            }
            catch (MySql.Data.MySqlClient.MySqlException SqEx)
            {
                throw new Exception(Utilities.ErrorCodes.ProcessException(SqEx, "", "", "",
                    Utilities.ErrorCodes.MySqlExceptionMsg(SqEx)));
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (_MyConnection != null)
                    {
                        if (_MyConnection.State == System.Data.ConnectionState.Open)
                            Task.Run(() => _MyConnection.CloseAsync());
                        _MyConnection.Dispose();
                    }
                }
                CloseHandle(handle);
                handle = IntPtr.Zero;
            }
            disposed = true;
        }

        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static bool CloseHandle(IntPtr handle);

        ~MySqlConnection()
        {
            Task.Run(() => Dispose(true));
        }
    }

    public class MySqlCommand : IDisposable
    {
        private IntPtr handle; // Pointer to an external unmanaged resource.
        private readonly MySql.Data.MySqlClient.MySqlConnection _MyConnection;      // Other managed resource this class uses.
        private MySql.Data.MySqlClient.MySqlDataAdapter _MyDataAdaptor;
        private readonly MySql.Data.MySqlClient.MySqlCommand _MyCommand;
        private bool disposed = false;  // Track whether Dispose has been called.

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public MySqlCommand(MySql.Data.MySqlClient.MySqlConnection MyConnection)
        {
            _MyConnection = MyConnection;
            _MyCommand = new MySql.Data.MySqlClient.MySqlCommand
            {
                Connection = _MyConnection
            };
        }
        public void Dispose()
        {
            Dispose(true);
            // Take yourself off the Finalization queue 
            // to prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }
        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the 
        // runtime from inside the finalizer and you should not reference 
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                if (disposing)
                {
                    if (_MyCommand != null)
                    {
                        if (_MyCommand.Parameters.Count > 0)
                            _MyCommand.Parameters.Clear();
                        _MyCommand.Connection.Close();
                        _MyCommand.Dispose();
                    }
                    if (_MyDataAdaptor != null)
                    {
                        _MyDataAdaptor.Dispose();
                    }
                }
                // Release unmanaged resources. If disposing is false, 
                // only the following code is executed.
                CloseHandle(handle);
                handle = System.IntPtr.Zero;
                // Note that this is not thread safe.
                // Another thread could start disposing the object
                // after the managed resources are disposed,
                // but before the disposed flag is set to true.
                // If thread safety is necessary, it must be
                // implemented by the client.
            }
            disposed = true;
        }
        // Use interop to call the method necessary  
        // to clean up the unmanaged resource.
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method 
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~MySqlCommand()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(true);
        }
        // Do not make this method virtual.
        // A derived class should not be allowed
        // to override this method.
        public void Close()
        {
            // Calls the Dispose method without parameters.
            Dispose();
        }


        /// <summary>
        /// Add a Parameter to current command object
        /// </summary>
        /// <param name="ParameterName">Parameter Name</param>
        /// <param name="Value">Parameter Value</param>
        public void Add_Parameter_WithValue(string ParameterName, object Value)
        {
            try
            {
                if (ParameterName.IndexOf("@") < 0)
                    ParameterName = "@" + ParameterName;
                _MyCommand.Parameters.AddWithValue(ParameterName, Value);
            }
            catch (MySql.Data.MySqlClient.MySqlException SqEx)
            {
                throw new Exception(SqEx.Message);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Add a Parameter to current command object
        /// </summary>
        /// <param name="ParameterName">Parameter Name</param>
        /// <param name="DataType">Parameter Data Type</param>
        /// <param name="Value">Parameter Value</param>
        public void Add_Parameter(string ParameterName, MySql.Data.MySqlClient.MySqlDbType DataType, object Value)
        {
            try
            {
                if (ParameterName.IndexOf("@") < 0)
                    ParameterName = "@" + ParameterName;
                _MyCommand.Parameters.Add(ParameterName, DataType).Value = Value;
            }
            catch (MySql.Data.MySqlClient.MySqlException SqEx)
            {
                throw new Exception(SqEx.Message);
            }
            catch (System.Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Add a query or commandtext to current command object
        /// </summary>
        /// <param name="CommandText">Pass Command Text here </param>
        public void Add_CommandText(string CommandText)
        {
            try
            {
                if (CommandText != null)
                    _MyCommand.CommandText = CommandText;
            }
            catch (MySql.Data.MySqlClient.MySqlException SqEx)
            {
                throw new Exception(SqEx.Message);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// <summary>
        /// Clears parameter from current command object
        /// </summary>

        public bool Clear_CommandParameter()
        {
            try
            {
                if (_MyCommand.Parameters.Count > 0)
                    _MyCommand.Parameters.Clear();
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException SqEx)
            {
                throw new Exception(SqEx.Message);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Add a transaction locktype to the current commention
        /// </summary>
        /// <param name="MyTransaction">Pass Transaction object </param>

        public void Add_Transaction(MySql.Data.MySqlClient.MySqlTransaction MyTransaction)
        {
            try
            {
                _MyCommand.Transaction = MyTransaction;
            }
            catch (MySql.Data.MySqlClient.MySqlException Sqex)
            {
                throw new Exception(Sqex.Message);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Execute query in the databaxse
        /// </summary>
        /// <param name="Query">Write either SQl Select statment or name  of Stored Procedure </param>
        /// <param name="CommandType">Specify command type as Text if you passed Text as Query or 
        /// StoredProcedure if you passed name of Stored Procedure as Query</param>
        private async Task<bool> Execute_Query_WithTransaction(string Query, System.Data.CommandType CmdType,
            MySql.Data.MySqlClient.MySqlTransaction MyTransaction,
            bool UseTransaction)
        {
            try
            {
                if (_MyConnection.State == System.Data.ConnectionState.Closed)
                {
                    await _MyConnection.OpenAsync();
                }
                _MyCommand.CommandText = Query;
                _MyCommand.CommandType = CmdType;
                if (UseTransaction == true)
                    _MyCommand.Transaction = MyTransaction;
                _MyCommand.CommandTimeout = 0;
                await _MyCommand.ExecuteNonQueryAsync();
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException Sqex)
            {
                throw new Exception(Utilities.ErrorCodes.ProcessException(Sqex, "", "", "", Utilities.ErrorCodes.MySqlExceptionMsg(Sqex)));
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

            finally
            {
                Close();
            }
        }

        /// <summary>
        /// Execute query in the databaxse
        /// </summary>
        /// <param name="Query">Write either SQl Select statment or name  of Stored Procedure </param>
        /// <param name="CommandType">Specify command type as Text if you passed Text as Query or 
        /// StoredProcedure if you passed name of Stored Procedure as Query</param>
        public async Task<bool> Execute_Query(string Query, System.Data.CommandType CommandType)
        {
            try
            {
                if (_MyConnection.State == System.Data.ConnectionState.Closed)
                {
                    await _MyConnection.OpenAsync();
                }
                _MyCommand.CommandText = Query;
                _MyCommand.CommandType = CommandType;
                _MyCommand.CommandTimeout = 0;
                var _result = await _MyCommand.ExecuteNonQueryAsync();
                return Convert.ToBoolean(_result);
            }
            catch (MySql.Data.MySqlClient.MySqlException Sqex)
            {
                throw new Exception(Utilities.ErrorCodes.ProcessException(Sqex, "", "", "", Utilities.ErrorCodes.MySqlExceptionMsg(Sqex)));
            }
            catch (System.Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
                Close();
            }
        }


        /// <summary>
        /// returns single string value from database
        /// </summary>
        /// <param name="Query">Write either SQl Select statment or name  of Stored Procedure </param>
        /// <param name="CommandType">Specify command type as Text if you passed Text as Query or StoredProcedure if you passed name of Stored Procedure as Query</param>

        public async Task<string> Select_Scalar(string Query, System.Data.CommandType CommandType)
        {
            try
            {
                if (_MyConnection.State == System.Data.ConnectionState.Closed)
                {
                    await _MyConnection.OpenAsync();
                }
                _MyCommand.CommandText = Query;
                _MyCommand.CommandType = CommandType;
                _MyCommand.CommandTimeout = 0;
                return Convert.ToString(_MyCommand.ExecuteScalar());
            }

            catch (MySql.Data.MySqlClient.MySqlException Sqex)
            {
                throw new Exception(Utilities.ErrorCodes.ProcessException(Sqex, "", "", "", Utilities.ErrorCodes.MySqlExceptionMsg(Sqex)));
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// returns single datatable from database
        /// </summary>
        /// <param name="Query">Write either SQl Select statment or name  of Stored Procedure </param>
        /// <param name="CommandType">Specify command type as Text if you passed Text as Query or StoredProcedure if you passed name of Stored Procedure as Query</param>

        public async Task<System.Data.DataTable> Select_Table(string Query, System.Data.CommandType CommandType)
        {
            System.Data.DataTable DT = new System.Data.DataTable();
            try
            {
                if (_MyConnection.State == System.Data.ConnectionState.Closed)
                {
                    await _MyConnection.OpenAsync();
                }
                _MyCommand.CommandText = Query;
                _MyCommand.CommandType = CommandType;
                _MyCommand.CommandTimeout = 0;

                _MyDataAdaptor = new MySql.Data.MySqlClient.MySqlDataAdapter(_MyCommand);
                await _MyDataAdaptor.FillAsync(DT);
                return DT;
            }
            catch (MySql.Data.MySqlClient.MySqlException Sqex)
            {
                throw new Exception(Utilities.ErrorCodes.ProcessException(Sqex, "", "", "", Utilities.ErrorCodes.MySqlExceptionMsg(Sqex)));
            }
            catch (System.Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
                Close();
            }
        }
        /// <summary>
        /// returns one or more datatable from database
        /// </summary>
        /// <param name="Query">Write either SQl Select statment or name  of Stored Procedure </param>
        /// <param name="CommandType">Specify command type as Text if you passed Text as Query or StoredProcedure if you passed name of Stored Procedure as Query</param>

        public async Task<System.Data.DataSet> Select_TableSet(string Query, System.Data.CommandType CommandType)
        {

            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                if (_MyConnection.State == System.Data.ConnectionState.Closed)
                {
                    await _MyConnection.OpenAsync();
                }
                _MyCommand.CommandText = Query;
                _MyCommand.CommandType = CommandType;
                _MyCommand.CommandTimeout = 0;

                _MyDataAdaptor = new MySql.Data.MySqlClient.MySqlDataAdapter(_MyCommand);
                await _MyDataAdaptor.FillAsync(DS);
                return DS;
            }

            catch (MySql.Data.MySqlClient.MySqlException Sqex)
            {
                throw new Exception(Utilities.ErrorCodes.ProcessException(Sqex, "", "", "", Utilities.ErrorCodes.MySqlExceptionMsg(Sqex)));
            }
            catch (System.Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// Author: Gautam Sharma
        /// Adds Parameter with value using Model Data values
        /// </summary>
        /// <typeparam name="T">Pass the Model Data here</typeparam>
        /// <param name="ModelData"></param>
        public void Add_Parameter_WithValue<T>(T ModelData) where T : class
        {
            try
            {
                Type temp = typeof(T);
                PropertyInfo[] propertyInfos = temp.GetProperties();

                foreach (var prop in propertyInfos)
                {
                    _MyCommand.Parameters.AddWithValue((prop.Name.IndexOf("@") < 0) ? $"@{prop.Name}" :
                        prop.Name, prop.GetValue(ModelData));
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException SqEx)
            {
                throw new Exception(SqEx.Message);
            }
            catch (System.Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// Author: Gautam Sharma
        /// Method to insert List/Single Data to a particular table
        /// <param name="SPName"> Provide the Stored Procedure name </param>
        /// <param name="ListData<Model>"> List of data (Model Values); Default - null</param>
        /// <param name="SingleRowData"> If only single row needs to be inserted, pass it here, Default - null</param>
        /// <param name="SPInitials"> If stored procedure parameter's values is given with some initials, pass it here, 
        /// default - null</param>
        /// </summary>
        public async Task<DataResponse> AddOrEditWithStoredProcedure<T>(string SPName, List<T> ListData,
            T SingleRowData, string SPInitials = "") where T : class
        {
            Type temp = typeof(T);

            PropertyInfo[] propertyInfos = temp.GetProperties();
            try
            {
                if (_MyConnection.State == System.Data.ConnectionState.Closed)
                {
                    await _MyConnection.OpenAsync();
                    _MyCommand.Connection = _MyConnection;
                }
                _MyCommand.CommandText = SPName;
                _MyCommand.CommandType = System.Data.CommandType.StoredProcedure;
                _MyCommand.CommandTimeout = 0;

                if (ListData != null)
                {
                    foreach (var item in ListData)
                    {
                        Clear_CommandParameter();
                        await InsertRecords(SPInitials, propertyInfos, item);
                    }
                }
                else if (SingleRowData != null)
                {
                    Clear_CommandParameter();
                    await InsertRecords(SPInitials, propertyInfos, SingleRowData);
                }
                return new DataResponse("Data Process Success", true);
            }
            catch (Exception ex)
            {
                return new DataResponse(ex.Message, false);
            }
            finally
            {
                Close();
            }
        }

        private async Task InsertRecords<T>(string SPInitials, PropertyInfo[] propertyInfos, T item) where T : class
        {
            foreach (var pro in propertyInfos)
            {
                var _itemValue = pro.GetValue(item);
                // Convert DateTime to MySql DateTime Format
                if (pro.PropertyType.Name == "DateTime")
                {
                    _itemValue = Convert.ToDateTime(_itemValue).ToString("yyyy-MM-dd");
                }
                else if (pro.PropertyType.Name == "Guid")
                {
                    _itemValue = _itemValue.ToString();
                }

                _MyCommand.Parameters.AddWithValue($"@{SPInitials}{pro.Name}", _itemValue);

            }

            await _MyCommand.ExecuteNonQueryAsync();

        }
    }
}
