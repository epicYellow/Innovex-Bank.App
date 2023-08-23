﻿using System;
using System.Diagnostics;
using System.Text.Json;
using Innovex_Bank.Models;
namespace Innovex_Bank.Services
{
	public class RestService: IRestService
	{
        // Define httpClient
        readonly HttpClient _client;
        readonly JsonSerializerOptions _serializerOptions;

		// Base API URL
		internal string baseUrl = "https://localhost:7230/api/StaffModels";

		// List of Staff
		public List<StaffModel> Items { get; private set; }

		// Creating httpClient
		public RestService()
		{
			_client = new HttpClient();

			_serializerOptions = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				WriteIndented = true
			};
		}

		public async Task<List<StaffModel>> RefreshDataAsync()
		{
			Items = new List<StaffModel>();

			Uri uri = new(string.Format(baseUrl, string.Empty));

			try
			{
				HttpResponseMessage response = await _client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					string content = await response.Content.ReadAsStringAsync();
					Items = JsonSerializer.Deserialize<List<StaffModel>>(content, _serializerOptions);
				}
			} catch (Exception ex)
			{
				Debug.WriteLine(@"\tERROR {0}", ex.Message);
			}

			return Items;
		}
	}
}
