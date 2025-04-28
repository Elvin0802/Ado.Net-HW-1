# ClassifiedsApp API Documentation

## Table of Contents
- [Overview](#overview)
- [Authentication](#authentication)
- [Ads](#ads)
- [Categories](#categories)
  - [Create Category](#create-category)
  - [Get All Categories](#get-all-categories)
  - [Get Category By ID](#get-category-by-id)
  - [Create Main Category](#create-main-category)
  - [Get All Main Categories](#get-all-main-categories)
  - [Get Main Category By ID](#get-main-category-by-id)
  - [Create Sub Category](#create-sub-category)
  - [Get All Sub Categories](#get-all-sub-categories)
  - [Get Sub Category By ID](#get-sub-category-by-id)
- [Locations](#locations)
  - [Create Location](#create-location)
  - [Get All Locations](#get-all-locations)
  - [Get Location By ID](#get-location-by-id)
  - [Delete Location](#delete-location)
- [Chat](#chat)
  - [Create Chat Room](#create-chat-room)
  - [Get Chat Rooms](#get-chat-rooms)
  - [Get Chat Room](#get-chat-room)
  - [Get Chat Messages](#get-chat-messages)
  - [Send Message](#send-message)
  - [Mark Messages As Read](#mark-messages-as-read)
- [Profile](#profile)
  - [Get User Data](#get-user-data)
  - [Get Active Ads](#get-active-ads)
  - [Get Pending Ads](#get-pending-ads)
  - [Get Expired Ads](#get-expired-ads)
  - [Get Rejected Ads](#get-rejected-ads)
  - [Get Selected Ads](#get-selected-ads)
- [Users](#users)
  - [Register](#register)
  - [Update Password](#update-password)
  - [Change Password](#change-password)
- [Reports](#reports)
  - [Create Report](#create-report)
  - [Get All Reports](#get-all-reports)
  - [Get Report By ID](#get-report-by-id)
  - [Get Reports By Ad ID](#get-reports-by-ad-id)
  - [Update Report Status](#update-report-status)
- [Schemas](#schemas)

---

## Overview
**Base URL**: `https://api.yoursite.com/api`  
**Version**: `v1.0`

Common Response Wrapper types:
- `Result`: `{ isSucceeded: boolean; message?: string; isFailed: boolean; }`
- `StringResult`: extends `Result` with `data: string` (e.g. JWT tokens)

---

## Authentication
*(As previously defined)*

---

## Ads
*(As previously defined)*

---

## Categories

### Create Category
- **Endpoint**: `POST /Categories/create/category`
- **Description**: Add a new top-level category.
- **Request Body** (`CreateCategoryCommand`):
  ```json
  { "name": "string" }
  ```
- **Response** (`Result`)

### Get All Categories
- **Endpoint**: `GET /Categories/all/category`
- **Description**: Retrieve paginated list of categories.
- **Query Parameters**:
  - `PageNumber` (int)
  - `PageSize` (int)
  - `SortBy` (string)
  - `IsDescending` (boolean)
- **Response** (`GetAllCategoriesQueryResponseResult`):
  ```json
  {
    "data": {
      "items": [ { CategoryDto }, ... ],
      "pageNumber": 1,
      "pageSize": 10,
      "totalCount": 100,
      "totalPages": 10
    }
  }
  ```

### Get Category By ID
- **Endpoint**: `GET /Categories/byId/category?Id={id}`
- **Description**: Fetch single category details including nested main categories.
- **Response** (`GetCategoryByIdQueryResponseResult`): `{ data: { item: CategoryDto } }`

### Create Main Category
- **Endpoint**: `POST /Categories/create/main-category`
- **Description**: Create a subcategory group under a top-level category.
- **Request Body** (`CreateMainCategoryCommand`):
  ```json
  { "name": "string", "parentCategoryId": "uuid" }
  ```
- **Response** (`Result`)

### Get All Main Categories
- **Endpoint**: `GET /Categories/all/main-category`
- **Query**: `?PageNumber=&PageSize=&SortBy=&IsDescending=`  
- **Response** (`GetAllMainCategoriesQueryResponseResult`)

### Get Main Category By ID
- **Endpoint**: `GET /Categories/byId/main-category?Id={id}`
- **Response** (`GetMainCategoryByIdQueryResponseResult`)

### Create Sub Category
- **Endpoint**: `POST /Categories/create/sub-category`
- **Description**: Add a specific attribute under a main category (e.g. Color, Size).
- **Request Body** (`CreateSubCategoryCommand`):
  ```json
  {
    "name": "string",
    "isRequired": true,
    "type": "String" | "Number" | "Boolean",
    "mainCategoryId": "uuid",
    "options": ["option1","option2"]
  }
  ```
- **Response** (`Result`)

### Get All Sub Categories
- **Endpoint**: `GET /Categories/all/sub-category`
- **Query**: `?PageNumber=&PageSize=&SortBy=&IsDescending=`  
- **Response** (`GetAllSubCategoriesQueryResponseResult`)

### Get Sub Category By ID
- **Endpoint**: `GET /Categories/byId/sub-category?Id={id}`
- **Response** (`GetSubCategoryByIdQueryResponseResult`)

---

## Locations

### Create Location
- **Endpoint**: `POST /Locations/Create`
- **Request Body** (`CreateLocationCommand`):
  ```json
  { "city": "string", "country": "string" }
  ```
- **Response** (`Result`)

### Get All Locations
- **Endpoint**: `GET /Locations/GetAll`
- **Description**: List all saved locations.
- **Response** (`GetAllLocationsQueryResponseResult`)

### Get Location By ID
- **Endpoint**: `GET /Locations/GetById?Id={id}`
- **Response** (`GetLocationByIdQueryResponseResult`)

### Delete Location
- **Endpoint**: `POST /Locations/Delete`
- **Request Body** (`DeleteLocationCommand`): `{ "id": "uuid" }`
- **Response** (`Result`)

---

## Chat

### Create Chat Room
- **Endpoint**: `POST /Chat/CreateChatRoom`
- **Request Body** (`CreateChatRoomCommand`): `{ "adId": "uuid" }`
- **Response** (`ChatRoomDtoResult`): `{ data: ChatRoomDto }
`

### Get Chat Rooms
- **Endpoint**: `POST /Chat/GetChatRooms`
- **Description**: Retrieve rooms for current user.
- **Response** (`GetChatRoomsByUserQueryResponseResult`)

### Get Chat Room
- **Endpoint**: `POST /Chat/GetChatRoom`
- **Request** (`GetChatRoomQuery`): `{ "roomId": "uuid" }`
- **Response** (`GetChatRoomQueryResponseResult`)

### Get Chat Messages
- **Endpoint**: `POST /Chat/GetChatMessages`
- **Request** (`GetChatMessagesByChatRoomQuery`): `{ "chatRoomId": "uuid" }`
- **Response** (`GetChatMessagesByChatRoomQueryResponseResult`)

### Send Message
- **Endpoint**: `POST /Chat/SendMessage`
- **Request** (`SendMessageCommand`):
  ```json
  { "chatRoomId": "uuid", "content": "string" }
  ```
- **Response** (`ChatMessageDtoResult`)

### Mark Messages As Read
- **Endpoint**: `POST /Chat/MarkMessagesAsRead`
- **Request** (`MarkMessagesAsReadCommand`): `{ "chatRoomId": "uuid" }`
- **Response** (`Result`)

---

## Profile

### Get User Data
- **Endpoint**: `POST /Profile/GetUserData`
- **Response** (`GetUserDataQueryResponseResult`): `{ data: AppUserDto }
`

### Get Active Ads
- **Endpoint**: `POST /Profile/GetActiveAds`
- **Response** (`GetAllAdsQueryResponseResult`)

### Get Pending Ads
- **Endpoint**: `POST /Profile/GetPendingAds`
- **Response** (`GetAllAdsQueryResponseResult`)

### Get Expired Ads
- **Endpoint**: `POST /Profile/GetExpiredAds`
- **Response** (`GetAllAdsQueryResponseResult`)

### Get Rejected Ads
- **Endpoint**: `POST /Profile/GetRejectedAds`
- **Response** (`GetAllAdsQueryResponseResult`)

### Get Selected Ads
- **Endpoint**: `POST /Profile/GetSelectedAds`
- **Response** (`GetAllSelectedAdsQueryResponseResult`)

---

## Users

### Register
- **Endpoint**: `POST /Users/register`
- **Request Body** (`RegisterCommand`):
  ```json
  { "name": "string", "email": "string", "phoneNumber": "string", "password": "string" }
  ```
- **Response** (`Result`)

### Update Password
- **Endpoint**: `POST /Users/update-password`
- **Request Body** (`UpdatePasswordCommand`):
  ```json
  { "userId": "uuid", "newPassword": "string" }
  ```
- **Response** (`Result`)

### Change Password (Authenticated)
- **Endpoint**: `POST /Users/change-password`
- **Request Body** (`ChangePasswordCommand`):
  ```json
  { "oldPassword": "string", "newPassword": "string", "newPasswordConfirm": "string" }
  ```
- **Response** (`Result`)

---

## Reports

### Create Report
- **Endpoint**: `POST /Reports/CreateReport`
- **Request Body** (`CreateReportCommand`):
  ```json
  { "adId": "uuid", "reason": "enum", "description": "string" }
  ```
- **Response** (`Result`)

### Get All Reports
- **Endpoint**: `GET /Reports/GetAllReports?status={status}`
- **Response** (`GetAllReportsQueryResponseResult`)

### Get Report By ID
- **Endpoint**: `GET /Reports/GetReportById/{id}`
- **Response** (`GetReportByIdQueryResponseResult`)

### Get Reports By Ad ID
- **Endpoint**: `GET /Reports/GetReportsByAdId/{adId}`
- **Response** (`GetReportsByAdIdQueryResponseResult`)

### Update Report Status
- **Endpoint**: `POST /Reports/UpdateReportStatus`
- **Request Body** (`UpdateReportStatusCommand`): `{ "reportId": "uuid", "newStatus": "enum" }`
- **Response** (`Result`)

---

## Schemas
Refer to `components/schemas` in the OpenAPI spec for full definitions of all request and response models, including:
- `CategoryDto`, `MainCategoryDto`, `SubCategoryDto`
- `LocationDto`
- `ChatRoomDto`, `ChatMessageDto`
- `AppUserDto`
- Pagination wrappers (`GetAll*QueryResponse`)
- Command/Query objects

*Import `swagger.json` into Swagger UI or Postman for live examples and testing.*

