﻿namespace TodoApp.Core.Dtos.Auth;

public class LoginResultDto
{
    public string Username { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
}