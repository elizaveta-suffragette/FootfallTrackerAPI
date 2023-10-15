# Footfall Data Aggregation API

## Task Description

At PFM, we collect and process footfall data collected by various sensors. This data is stored in 15-minute time slots per sensor. API that can provide this data in different aggregations: hourly, daily, and weekly. The API should return accurate data as the correctness of the data is crucial for our customers.

## How to Use the API

### Endpoint

You can access the API through the following endpoint:

- **GET /api/aggregation**

### Parameters

The API expects the following parameters in the query string:

- `timeframe`: This should be one of the following values: "hourly", "daily", or "weekly". It specifies the desired aggregation level.
- `filePath`: The path to the CSV file containing the footfall data.
- `startDate`: The start date for the data you want to retrieve.
- `endDate`: The end date for the data you want to retrieve.

### Example Request

```http
GET /api/aggregation?timeframe=daily&filePath=YourFilePath\footfall_data.csv&startDate=2023-01-01T00:00:00&endDate=2023-01-02T00:00:00
```
