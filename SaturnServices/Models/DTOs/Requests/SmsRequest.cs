﻿namespace SaturnServices.Models.DTOs.Requests;

public class SmsRequest
{
    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; } = null!;
    [JsonPropertyName("otpCode")]
    public int OTPCode { get; set; }
}
