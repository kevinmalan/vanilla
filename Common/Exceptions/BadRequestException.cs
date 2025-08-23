﻿namespace Common.Exceptions
{
    public class BadRequestException(string message, object? data = null) : BaseException(message, data)
    {
    }
}