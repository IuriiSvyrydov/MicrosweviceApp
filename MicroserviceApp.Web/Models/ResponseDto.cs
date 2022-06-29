﻿namespace MicroserviceApp.Web.Models
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
        public string DisplayMessages { get; set; } = "";
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}
