namespace ScupTel.API.DataTransferObject
{
    public class ApiStatusDto
    {
        public ApiStatusDto(string serviceName, string status, string version)
        {
            ServiceName = serviceName;
            Status = status;
            Version = version;
        }

        public string ServiceName;
        public string Status;
        public string Version;
    }
}
