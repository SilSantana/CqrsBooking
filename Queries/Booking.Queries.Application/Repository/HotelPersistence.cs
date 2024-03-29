﻿using Booking.Infrastructure.Storage.SqlServer;
using Booking.Queries.Application.Hotel.ReadModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking.Queries.Application.Repository
{
    internal class HotelPersistence : RepositoryBase
    {        
        public HotelPersistence(ISqlServerStoreHolder sqlServerStoreHolder) : base(sqlServerStoreHolder)
        {
        }

        public async Task<IEnumerable<HotelListItem>> RetrieveHotels()
        {
            return await HandleConnection(async (connection) =>
            {
                return await connection.QueryAsync<HotelListItem>(@"
                                                                SELECT H.[Code]
                                                                        ,H.[Name]
                                                                        ,[StarsOfCategory]
                                                                        ,[StarsOfRating]
                                                                        ,[AddressStreet]
                                                                        ,[AddressDistrict]
                                                                        ,[AddressCity]
                                                                        ,[AddressCountry]
                                                                        ,[ZipCode]
                                                                        ,[ContactMobile]
                                                                        ,[ContactPhone]
                                                                        ,[ContactEmail]
                                                                     , (SELECT COUNT(1) FROM [EmergingBooking].[dbo].[Rooms] AS R WHERE r.HotelCode = H.Code) AS Rooms
                                                                    FROM [EmergingBooking].[dbo].[Hotels] AS H
                                                                ");
            });
        }

        internal async Task<CurrentContacts> RetrieveCurrentContacts(Guid hotelCode)
        {
            return await HandleConnection(async (connection) =>
            {
                return await connection
                    .QueryFirstAsync<CurrentContacts>(@"
                                                SELECT ContactEmail as Email,
                                                        ContactPhone as Phone,
                                                        ContactMobile as Mobile
                                                FROM Hotels
                                                Where Code = @hotelCode",
                                                new { hotelCode });
            });
        }

        internal async Task<CurrentAddress> RetrieveCurrentAddress(Guid hotelCode)
        {
            return await HandleConnection(async (connection) =>
            {
                return await connection
                    .QueryFirstAsync<CurrentAddress>(@"
                                                    SELECT AddressStreet as Street,
                                                           AddressDistrict as District,
                                                           AddressCity as City,
                                                           AddressCountry as Country,
                                                           ZipCode
                                                    FROM Hotels
                                                    Where Code = @hotelCode",
                                                    new { hotelCode });
            });
        }

        public async Task<IEnumerable<RoomListItem>> RetrieveRooms(Guid hotelCode)
        {
            return await HandleConnection(async (connection) =>
            {
                return await connection
                    .QueryAsync<RoomListItem>("SELECT * FROM Rooms Where HotelCode = @hotelCode", new { hotelCode });
            });
        }

        public async Task<IEnumerable<AvailableRooms>> RetrieveAvailableRooms(DateTime checking, DateTime checkout)
        {
            var sql = @"
                        SELECT
                            H.Code AS HotelCode
	                        , H.Name AS HotelName
                            , H.StarsOfCategory
	                        , COALESCE(MIN(RS.HotelAddress),
			                           MIN(H.AddressStreet + ', ' +
                                           H.AddressDistrict + ', ' +
                                           H.AddressCity + ', ' +
                                           H.AddressCountry + ' - ' +
                                           CONVERT(VARCHAR(10), H.ZipCode))) AS HotelAddress
	                        , R.Name AS RoomName
	                        , R.Code AS RoomCode
	                        , MAX(RS.Checkin) AS UnavailableDateFrom
	                        , MAX(RS.Checkout) AS UnavailableDateTo
	                        , (MAX(R.AvailableQuantity) - Count(R.Name)) AS [Availability]
                        FROM
	                        Hotels AS H
		                        JOIN Rooms AS R ON H.Code = R.HotelCode
		                        LEFT JOIN Reservations AS RS ON R.Code = RS.RoomCode
                        WHERE R.Code NOT IN
                        (
	                        SELECT
                                RS2.RoomCode
	                        FROM
                                Reservations AS RS2
	                        WHERE @DateCheckin BETWEEN RS2.Checkin AND RS2.Checkout
	                        OR @DateCheckout BETWEEN RS2.Checkin AND RS2.Checkout
                        )
                        GROUP BY
                              H.Code
	                        , H.Name
                            , H.StarsOfCategory
	                        , R.Code
	                        , R.Name
                        ";

            return await HandleConnection(async (connection) =>
            {
                return await connection
                    .QueryAsync<AvailableRooms>(sql,
                    new
                    {
                        DateCheckin = checking,
                        DateCheckout = checkout
                    });
            });
        }
    }
}
