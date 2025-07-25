﻿namespace Domain.UsersAdmin;
public class UserDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string AddressCode { get; set; }
    public bool IsDelete { get; set; }
    public bool IsAdmin { get; set; }
}