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

- **POST /api/vehicles/AddVehicle** - Add a vehicle
    - **Request:**
      ```json
      {
          "manufacturer": "Toyota",
          "model": "RAV4",
          "year": 2020,
          "startingBid": 1000.12,
          "type": "Suv",
          "numberOfDoors": 0,
          "numberOfSeats": 4,
          "loadCapacity": 0
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
          "numberOfDoors": 0,
          "numberOfSeats": 4,
          "loadCapacity": 0
      }
      ```

- **PUT /api/vehicles/UpdateVehicle** - Update a vehicle
    - **Request:**
      ```json
      {
          "id": "ff1e51c-774a-4a41-b371-374df919a2c3",
          "manufacturer": "Hyundai",
          "model": "CR-40",
          "year": 2019,
          "startingBid": 5555.84,
          "type": "Hatchback",
          "numberOfDoors": 5,
          "numberOfSeats": 0,
          "loadCapacity": 0
      }
      ```
    - **Response:**
      ```json
      {
          "id": "ff1e51c-774a-4a41-b371-374df919a2c3",
          "manufacturer": "Hyundai",
          "model": "CR-40",
          "year": 2019,
          "startingBid": 5555.84,
          "type": "Hatchback",
          "numberOfDoors": 5,
          "numberOfSeats": 0,
          "loadCapacity": 0
      }
      ```

- **GET /api/vehicles/GetAllVehicles** - Get all vehicles
    - **Response:**
      ```json
      {
          "getVehicles": [
             {
               "id": "fff1e51c-774a-4a41-b371-374df919a2c3",
               "manufacturer": "Toyota",
               "model": "RAV4",
               "year": 2020,
               "startingBid": 1000.12,
               "type": "Suv",
               "numberOfDoors": null,
               "numberOfSeats": 4,
               "loadCapacity": null
             }
          ]
      }
      ```

- **GET /api/vehicles/GetVehiclesWithFilters/{year}/{type}/{manufacturer}/{model}** - Get vehicles with filters(All filters are optional)
    - **Request:**
      ```
      url: /api/vehicles/GetVehiclesWithFilters/2020/Suv/Toyota/RAV4
      ```
    - **Response:**
      ```json
      {
          "getVehicles": [
             {
               "id": "fff1e51c-774a-4a41-b371-374df919a2c3",
               "manufacturer": "Hyundai",
               "model": "CR-40",
               "year": 2019,
               "startingBid": 5555.84,
               "type": "Hatchback",
               "numberOfDoors": 5,
               "numberOfSeats": null,
               "loadCapacity": null
             }
          ]
      }
      ```

- **DELETE /api/vehicles/RemoveVehicle/{id}** - Delete a vehicle
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

- **POST /api/auctions/StartAuction** - Start an auction
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
             "manufacturer": "Hyundai",
             "model": "CR-40",
             "year": 2019,
             "startingBid": 5555.84
          },
          "highestBid": 5555.84
      }
      ```

- **POST /api/auctions/PlaceBid** - Place a bid
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

- **GET /api/auctions/GetAuctions** - Get all auctions
    - **Response:**
      ```json
      {
          "getAuctions": [
             {
               "id": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
               "vehicle": {
                  "id": "fff1e51c-774a-4a41-b371-374df919a2c3",
                  "manufacturer": "Hyundai",
                  "model": "CR-40",
                  "year": 2019,
                  "startingBid": 5555.84
               },
               "isActive": true,
               "bids": [
                  {
                      "id": "b06ae131-dfb7-4105-bd4f-ba9b2d471849",
                      "bidderId": "1",
                      "auctionId": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
                      "amount": 8000
                  }
               ],
               "highestBid": 5555.84,
               "highestBidder": ""
             }
          ]
      }
      ```

- **GET /api/auctions/GetActiveAuctions** - Get all active auctions
    - **Response:**
      ```json
      {
          "getAuctions": [
             {
               "id": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
               "vehicle": {
                  "id": "fff1e51c-774a-4a41-b371-374df919a2c3",
                  "manufacturer": "Hyundai",
                  "model": "CR-40",
                  "year": 2019,
                  "startingBid": 5555.84
               },
               "isActive": true,
               "bids": [
                  {
                      "id": "b06ae131-dfb7-4105-bd4f-ba9b2d471849",
                      "bidderId": "1",
                      "auctionId": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
                      "amount": 8000
                  }
               ],
               "highestBid": 5555.84,
               "highestBidder": ""
             }
          ]
      }
      ```

- **POST /api/auctions/EndAuction** - Close an auction
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

- **GET /api/auctions/GetClosedAuctions** - Get all closed auctions
    - **Response:**
      ```json
      {
          "getAuctions": [
             {
               "id": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
               "vehicle": {
                  "id": "fff1e51c-774a-4a41-b371-374df919a2c3",
                  "manufacturer": "Hyundai",
                  "model": "CR-40",
                  "year": 2019,
                  "startingBid": 5555.84
               },
               "isActive": false,
               "bids": [
                  {
                      "id": "b06ae131-dfb7-4105-bd4f-ba9b2d471849",
                      "bidderId": "1",
                      "auctionId": "222b7630-ca22-4ec0-8be0-cf1e34d1f8d2",
                      "amount": 8000
                  }
               ],
               "highestBid": 8000,
               "highestBidder": "1"
             }
          ]
      }
      ```