﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ExceptionHandling.ExceptionManagement
{
    public class ExceptionModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
