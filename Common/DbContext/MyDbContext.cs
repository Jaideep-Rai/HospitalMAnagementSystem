namespace Common.DbContext
{
    public class MyDbContext
    {
        public readonly MySqlConnection _MyConnection = null;
        public readonly MySqlCommand Await_MyCommand = null;
        public MyDbContext()
        {
            _MyConnection = new MySqlConnection();
            Await_MyCommand = new MySqlCommand(_MyConnection._MyConnection);
        }
        public void Close_MyDbContext()
        {
            if (Await_MyCommand != null)
                Await_MyCommand.Close();
            if (_MyConnection != null)
                _MyConnection.Dispose();
        }
    }
}
