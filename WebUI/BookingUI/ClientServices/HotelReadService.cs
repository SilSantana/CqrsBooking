﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BookingUI.Models;
using BookingUI.Models.Management;

namespace BookingUI.ClientServices
{
    public class HotelReadService
    {
        private readonly HttpClient HotelClient;

        public HotelReadService(HttpClient client)
        {
            HotelClient = client;
        }

        internal async Task<IEnumerable<RegisteredRoom>> GetRegisteredRooms(Guid hotelCode)
        {
            try
            {
                var relativePathEndpoint = string.Format(ClientServiceEndpoints.HotelEndpoints.ListRegisteredRooms, hotelCode);
                var response = await HotelClient.GetAsync(relativePathEndpoint);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<IReadOnlyCollection<RegisteredRoom>>();
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }

        internal async Task<IEnumerable<AvailableRooms>> GetAvailableRooms(DateTime checking, DateTime checkout)
        {
            try
            {
                var relativePathEndpoint = string.Format(ClientServiceEndpoints.HotelEndpoints.AvailableRooms, checking, checkout);
                var response = await HotelClient.GetAsync(relativePathEndpoint);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<IReadOnlyCollection<AvailableRooms>>();
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }

        internal async Task<IEnumerable<RegisteredHotel>> GetRegisteredHotels()
        {
            try
            {
                var response = await HotelClient.GetAsync(ClientServiceEndpoints.HotelEndpoints.ListRegisteredHotels);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<IReadOnlyCollection<RegisteredHotel>>();
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }

        internal async Task<CurrentAddress> GetCurrentAddress(Guid hotelCode)
        {
            try
            {
                var relativePathEndpoint = string.Format(ClientServiceEndpoints.HotelEndpoints.CurrentAddress, hotelCode);
                var response = await HotelClient.GetAsync(relativePathEndpoint);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<CurrentAddress>();
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }

        internal async Task<CurrentContacts> GetCurrentContacts(Guid hotelCode)
        {
            try
            {
                var relativePathEndpoint = string.Format(ClientServiceEndpoints.HotelEndpoints.CurrentContacts, hotelCode);
                var response = await HotelClient.GetAsync(relativePathEndpoint);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<CurrentContacts>();
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }
    }
}
