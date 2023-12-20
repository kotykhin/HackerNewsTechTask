# ASP.NET Core Hacker News API Gateway

## Project Overview

This project is an ASP.NET Core Web API that serves as a gateway to the Hacker News API. It provides endpoints to retrieve top stories from Hacker News, sorted by their score. The project utilizes Refit, an automatic type-safe REST library, to simplify calling external HTTP services. Along with Refit, it leverages Mapster for efficient object-to-object mapping between Hacker News API responses and our internal DTOs (Data Transfer Objects). Enhanced performance and error handling are achieved through asynchronous programming and concurrency control.

## Features

- **Fetch Top Stories**: Retrieve the top 'n' stories from Hacker News, sorted by score.
- **Refit Integration**: Uses Refit for type-safe, maintainable, and concise API calls.
- **Efficient API Calls**: Uses asynchronous programming and concurrency control for efficient API interaction under high load.
- **Error Handling**: Robust error handling for third-party API interactions.
- **Object Mapping**: Utilizes Mapster for clean and maintainable object mapping.

## Getting Started

### Prerequisites

- .NET 6.0 SDK
- Visual Studio 2019 or later (or another compatible IDE)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/kotykhin/HackerNewsTechTask.git

2. Navigate to the project directory:
   ```bash
   cd HackerNewsTechTask
   
3. Restore the .NET packages:
   ```bash 
   dotnet restore
   
4. Run the application using Visual Studio or via the command line:
   ```bash
   dotnet run

The API will be available at http://localhost:5029.

## Author's assumptions

- First of all, the api should handle high load, that's why asynchronous and concurrency features were added.
- On the other hand, we should not overload 3rd party API (Hacker news API in this case), that's why the concurrency control was added. It was implemented using Semaphore and controls number of parallel requests to API. By default it's 10.
- For simplifying integration with 3rd party API it was used Refit library. It's very simple to implement and maintain.
- For the mapping between models was used Mapster library. It's faster alternative to the Automapper. Overall, if aim is to get the best performance it's best to use manual mappers, but it's due to business requirements.
- Also it was used built-in ASP.NET Core logger for error handling. All basic infrastructure is done, so it can be easily substituted by any other logger like Serilog or NLog, and changed logging destination (By default it's logging to debug console).

## Author's supposed improvements

- First, add some tests. Unit test for logic testing, integration test for 3rd party API.
- Additionally, it could be a great idea to enhance monitoring and logging. As was mentioned in a section before, logging can be replaced with more complex solution to have a clear monitoring through DB or even some cloud based like AWS Cloudwatch.
- Also, it's good to add some security to the project. First step, adding basic authentication and authorization to the api.
- To handle overload of api, we can introduce rate limiter. It will also improve our security and partially protect API from the DDOS attacks.
- Also, to handle overload and improve performance, it can be a good idea to implement some architectural tweaks. For example:
  - Use proper cloud architecture, load balancer is a classic scenario in this case.
  - Implementing some caching mechanism of 3rd party API calls.
  - Introduce message bus and achieve some type of distributed system. Move some API calls to distributed microservice
