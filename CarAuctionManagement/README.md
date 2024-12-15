# CarAuctionManagement - BCA Code Challenge

## Description

This project is a code challenge for BCA. The project is a car auction management system. The system allows users to
create, update, delete, and view cars in the inventory and open, close, and view active and inactive auctions.
The system also allows users to bid on car auctions.

## Technologies

- C#
- .NET 8.0

## Prerequisites

- .NET 8.0 SDK
- An IDE such as Visual Studio, Visual Studio Code, or JetBrains Rider

## Installation

1. Clone the repository: `git clone https://github.com/bernardesvini/CarAuctionManagement.git` or download the zip file.
2. Open the solution in your preferred IDE.
3. Restore the dependencies: `dotnet restore`.
4. Build the project: `dotnet build`.

## Running the Project

1. Run the project: `dotnet run`.
2. The project will open in the browser.

## Running Tests

1. Navigate to the test project directory: `cd CarAuctionManagement.Tests`.
2. Run the tests: `dotnet test`.

## Usage - Endpoints

### Vehicles

- **POST /vehicles/AddVehicle** - Add a vehicle
    - **Request:**
      ```json
      {
          "manufacturer": "Toyota",
          "model": "RAV4",
          "year": 2020,
          "startingBid": 1000.12,
          "type": "Suv",
          "numberOfDoors": 4,
          "numberOfSeats": 5,
          "loadCapacity": 500
      }
      ```
    - **Response:**
      ```json
      {
          "id": "fff1e51c-774a-4a41-b371-374df919a2c3",
          "manufacturer": "Toyota",
          "model": "RAV4",
          "year": 2020,
          "startingBid": 1000.12,
          "type": "Suv",
          "numberOfDoors": 4,
          "numberOfSeats": 5,
          "loadCapacity": 500
      }
      ```

- **PUT /vehicles/UpdateVehicle** - Update a vehicle
    - **Request:**
      ```json
      {
          "id": "fff1e51c-774a-4a41-b371-374df919a2c3",
          "manufacturer": "Hyundai",
          "model": "CR-40",
          "year": 2019,
          "startingBid": 5555.84,
          "type": "Hatchback",
          "numberOfDoors": 5,
          "numberOfSeats": 4,
          "loadCapacity": 300
      }
      ```
    - **Response:**
      ```json
      {
          "id": "fff1e51c-774a-4a41-b371-374df919a2c3",
          "manufacturer": "Hyundai",
          "model": "CR-40",
          "year": 2019,
          "startingBid": 5555.84,
          "type": "Hatchback",
          "numberOfDoors": 5,
          "numberOfSeats": 4,
          "loadCapacity": 300
      }
      ```

- **GET /vehicles/GetVehicles** - Get all vehicles
    - **Response:**
      ```json
      {
          "vehicles": [
             {
               "id": "fff1e51c-774a-4a41-b371-374df919a2c3",
               "manufacturer": "Toyota",
               "model": "RAV4",
               "year": 2020,
               "startingBid": 1000.12,
               "type": "Suv",
               "numberOfDoors": 4,
               "numberOfSeats": 5,
               "loadCapacity": 500
             }
          ]
      }
      ```

- **GET /vehicles/GetVehiclesWithFilters** - Get vehicles with filters (All filters are optional)
    - **Request:**
      ```
      url: /vehicles/GetVehiclesWithFilters?year=2020&type=Suv&manufacturer=Toyota&model=RAV4
      ```
    - **Response:**
      ```json
      {
          "vehicles": [
             {
               "id": "fff1e51c-774a-4a41-b371-374df919a2c3",
               "manufacturer": "Toyota",
               "model": "RAV4",
               "year": 2020,
               "startingBid": 1000.12,
               "type": "Suv",
               "numberOfDoors": 4,
               "numberOfSeats": 5,
               "loadCapacity": 500
             }
          ]
      }
      ```

- **DELETE /vehicles/RemoveVehicle** - Delete a vehicle
    - **Request:**
      ```json
      {
          "id": "fff1e51c-774a-4a41-b371-374df919a2c3"
      }
      ```
    - **Response:**
      ```
      200 OK
      ```

### Auctions

- **POST /auctions/StartAuction** - Start an auction
    - **Request:**
      ```json
      {
          "vehicleId": "fff1e51c-774a-4a41-b371-374df919a2c3"
      }
      ```
    - **Response:**
      ```json
      {
          "id": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
          "vehicle": {
             "id": "fff1e51c-774a-4a41-b371-374df919a2c3",
             "manufacturer": "Toyota",
             "model": "RAV4",
             "year": 2020,
             "startingBid": 1000.12
          },
          "highestBid": 1000.12
      }
      ```

- **POST /auctions/PlaceBid** - Place a bid
    - **Request:**
      ```json
      {
          "auctionId": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
          "bidderId": "1",
          "amount": 8000.00
      }
      ```
    - **Response:**
      ```json
      {
          "auctionId": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
          "bidderId": "1",
          "amount": 8000.00
      }
      ```

- **GET /auctions/GetAuctions** - Get all auctions
    - **Response:**
      ```json
      {
          "auctions": [
             {
               "id": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
               "vehicle": {
                  "id": "fff1e51c-774a-4a41-b371-374df919a2c3",
                  "manufacturer": "Toyota",
                  "model": "RAV4",
                  "year": 2020,
                  "startingBid": 1000.12
               },
               "isActive": true,
               "bids": [
                  {
                      "id": "b06ae131-dfb7-4105-bd4f-ba9b2d471849",
                      "bidderId": "1",
                      "auctionId": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
                      "amount": 8000.00
                  }
               ],
               "highestBid": 8000.00,
               "highestBidder": "1"
             }
          ]
      }
      ```

- **GET /auctions/GetActiveAuctions** - Get all active auctions
    - **Response:**
      ```json
      {
          "auctions": [
             {
               "id": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
               "vehicle": {
                  "id": "fff1e51c-774a-4a41-b371-374df919a2c3",
                  "manufacturer": "Toyota",
                  "model": "RAV4",
                  "year": 2020,
                  "startingBid": 1000.12
               },
               "isActive": true,
               "bids": [
                  {
                      "id": "b06ae131-dfb7-4105-bd4f-ba9b2d471849",
                      "bidderId": "1",
                      "auctionId": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
                      "amount": 8000.00
                  }
               ],
               "highestBid": 8000.00,
               "highestBidder": "1"
             }
          ]
      }
      ```

- **GET /auctions/GetClosedAuctions** - Get all closed auctions
    - **Response:**
      ```json
      {
          "auctions": [
             {
               "id": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
               "vehicle": {
                  "id": "fff1e51c-774a-4a41-b371-374df919a2c3",
                  "manufacturer": "Toyota",
                  "model": "RAV4",
                  "year": 2020,
                  "startingBid": 1000.12
               },
               "isActive": false,
               "bids": [
                  {
                      "id": "b06ae131-dfb7-4105-bd4f-ba9b2d471849",
                      "bidderId": "1",
                      "auctionId": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
                      "amount": 8000.00
                  }
               ],
               "highestBid": 8000.00,
               "highestBidder": "1"
             }
          ]
      }
      ```

- **POST /auctions/EndAuction** - Close an auction
    - **Request:**
      ```json
      {
          "auctionId": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2"
      }
      ```
    - **Response:**
      ```
      200 OK
      ```

- **GET /auctions/GetAuctionById** - Get auction by ID
    - **Request:**
      ```
      url: /auctions/GetAuctionById?id=222b7630-ca22-4ec0-8be0-cf1e34d1f8d2
      ```
    - **Response:**
      ```json
      {
          "id": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
          "vehicle": {
             "id": "fff1e51c-774a-4a41-b371-374df919a2c3",
             "manufacturer": "Toyota",
             "model": "RAV4",
             "year": 2020,
             "startingBid": 1000.12
          },
          "isActive": true,
          "bids": [
             {
                 "id": "b06ae131-dfb7-4105-bd4f-ba9b2d471849",
                 "bidderId": "1",
                 "auctionId": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
                 "amount": 8000.00
             }
          ],
          "highestBid": 8000.00,
          "highestBidder": "1"
      }
      ```

### Bidders

- **POST /bidders/CreateBidder** - Create a bidder
    - **Request:**
      ```json
      {
          "name": "John Doe",
          "email": "john.doe@example.com"
      }
      ```
    - **Response:**
      ```json
      {
          "id": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
          "name": "John Doe",
          "email": "john.doe@example.com"
      }
      ```

- **GET /bidders/GetBidders** - Get all bidders
    - **Response:**
      ```json
      {
          "bidders": [
             {
               "id": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
               "name": "John Doe",
               "email": "john.doe@example.com"
             }
          ]
      }
      ```

- **GET /bidders/GetActiveBidders** - Get all active bidders
    - **Response:**
      ```json
      {
          "bidders": [
             {
               "id": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
               "name": "John Doe",
               "email": "john.doe@example.com"
             }
          ]
      }
      ```

- **GET /bidders/GetInactiveBidders** - Get all inactive bidders
    - **Response:**
      ```json
      {
          "bidders": [
             {
               "id": "b06ae131-dfb7-4105-bd4f-ba9b2d471849",
               "name": "Jane Doe",
               "email": "jane.doe@example.com"
             }
          ]
      }
      ```

- **GET /bidders/GetBidderById** - Get bidder by ID
    - **Request:**
      ```
      url: /bidders/GetBidderById?bidderId=1
      ```
    - **Response:**
      ```json
      {
          "id": "b06ae131-dfb7-4105-bd4f-ba9b2d471849",
          "name": "John Doe",
          "email": "john.doe@example.com"
      }
      ```

- **PUT /bidders/UpdateBidder** - Update a bidder
    - **Request:**
      ```json
      {
          "id": "b06ae131-dfb7-4105-bd4f-ba9b2d471849",
          "name": "John Smith",
          "email": "john.smith@example.com"
      }
      ```
    - **Response:**
      ```json
      {
          "id": "b06ae131-dfb7-4105-bd4f-ba9b2d471849",
          "name": "John Smith",
          "email": "john.smith@example.com"
      }
      ```

- **DELETE /bidders/RemoveBidder** - Delete a bidder
    - **Request:**
      ```json
      {
          "id": "b06ae131-dfb7-4105-bd4f-ba9b2d471849"
      }
      ```
    - **Response:**
      ```
      200 OK
      ```
