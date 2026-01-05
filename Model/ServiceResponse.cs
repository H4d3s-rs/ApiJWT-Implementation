using System;

namespace ex3___ApiLoginECors.Model;

public class ServiceResponse<T>
{
    public T? data { get; set; }

    public string message { get; set; }

    public bool success { get; set; }

}
