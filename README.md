# ClassifiedsApp API Documentation

## Table of Contents
- [Overview](#overview)
- [Authentication](#authentication)
- [Ads](#ads)
  - [Create Ad](#create-ad)
  - [Get All Ads](#get-all-ads)
  - [Get Ad By ID](#get-ad-by-id)
  - [Update Ad](#update-ad)
  - [Delete Ad (Soft)](#delete-ad-soft)
  - [Select/Unselect Ad](#selectunselect-ad)
  - [Featured Ads](#featured-ads)
  - [Change Ad Status](#change-ad-status)
- [Categories](#categories)
  - [Main Categories](#main-categories)
  - [Sub Categories](#sub-categories)
- [Locations](#locations)
- [Chat](#chat)
- [Profile](#profile)
- [Reports](#reports)
- [Users](#users)
- [Schemas](#schemas)

---

## Overview
**Base URL**: `https://api.yoursite.com/api`  
**Version**: `v1.0`  

This documentation covers all endpoints exposed by the ClassifiedsApp backend, grouped by feature area. Each section details URL, HTTP method, headers, request/response schemas, and usage examples.

---

## Authentication

### Login
- **Endpoint**: `POST /Auth/Login`  
- **Description**: Authenticate user and receive JWT tokens.  
- **Request Body** (JSON):
  ```json
  {
    "email": "string",
    "password": "string"
  }
  ```
- **Response** (`StringResult`): Contains `data` field with access token and refresh token.  
- **Status**: `200 OK`

### Refresh Token Login
- **Endpoint**: `POST /Auth/RefreshTokenLogin`  
- **Description**: Obtain new access token using a valid refresh token in header.  
- **Headers**:
  - `Authorization: Bearer {refreshToken}`
- **Response** (`StringResult`): New JWT tokens.  
- **Status**: `200 OK`

### Logout
- **Endpoint**: `POST /Auth/Logout`  
- **Description**: Invalidate current refresh token.  
- **Headers**:
  - `Authorization: Bearer {accessToken}`
- **Response** (`Result`): Success/failure message.  
- **Status**: `200 OK`

### Reset Password
- **Endpoint**: `POST /Auth/reset-password`  
- **Description**: Initiate password reset by sending email with token.  
- **Request Body** (JSON):
  ```json
  {
    "email": "string"
  }
  ```
- **Response** (`Result`): Confirmation message.  
- **Status**: `200 OK`

### Confirm Reset Token
- **Endpoint**: `POST /Auth/confirm-reset-token`  
- **Description**: Validate reset token and set new password.  
- **Request Body** (JSON):
  ```json
  {
    "userId": "uuid",
    "resetToken": "string"
  }
  ```
- **Response** (`Result`): Success/failure.  
- **Status**: `200 OK`

---

## Ads

### Create Ad
- **Endpoint**: `POST /Ads/Create`  
- **Content-Type**: `multipart/form-data`  
- **Description**: Create a new classified ad with images and subcategory values.  
- **Form Data Parameters**:
  | Field                 | Type     | Description                             |
  |-----------------------|----------|-----------------------------------------|
  | `Title`               | string   | Ad title                                |
  | `Description`         | string   | Detailed description                    |
  | `Price`               | number   | Price value                             |
  | `IsNew`               | boolean  | Condition flag                          |
  | `CategoryId`          | uuid     | Parent category identifier              |
  | `MainCategoryId`      | uuid     | Main category identifier                |
  | `LocationId`          | uuid     | Location identifier                     |
  | `SubCategoryValues`   | array    | List of `{ subCategoryId, value }`      |
  | `Images`              | file[]   | Image files (.jpg, .png)                |
- **Response** (`Result`): Operation result with `true/false` status.  
- **Status**: `200 OK`

### Get All Ads
- **Endpoint**: `POST /Ads/GetAll`  
- **Description**: Returns paged list of ads matching filters.  
- **Request Body** (`GetAllAdsQuery`):
  ```json
  {
    "pageNumber": 1,
    "pageSize": 20,
    "sortBy": "price",
    "isDescending": false,
    "searchTitle": "bike",
    "minPrice": 100,
    "maxPrice": 1000,
    "categoryId": "uuid",
    "isFeatured": false
  }
  ```
- **Response** (`GetAllAdsQueryResponseResult`): Contains `data` with page info and `items` array of `AdPreviewDto`.
- **Status**: `200 OK`

### Get Ad By ID
- **Endpoint**: `GET /Ads/GetById?Id={adId}&CurrentAppUserId={userId}`  
- **Description**: Fetch full ad details including owner, images, and subcategory values.  
- **Query Parameters**:
  | Name                 | Type  | Required | Description                    |
  |----------------------|-------|----------|--------------------------------|
  | `Id`                 | uuid  | Yes      | Ad identifier                  |
  | `CurrentAppUserId`   | uuid  | No       | For `isOwner` flag evaluation |
- **Response** (`GetAdByIdQueryResponseResult`): `data.item` is `AdDto`.
- **Status**: `200 OK`

### Update Ad
- **Endpoint**: `POST /Ads/Update`  
- **Description**: Update ad fields by sending `UpdateAdCommand`.  
- **Request Body** (`UpdateAdCommand`):
  ```json
  {
    "id": "uuid",
    "title": "string",
    "description": "string",
    "price": 150,
    "isNew": true
  }
  ```
- **Response** (`Result`): Success flag.
- **Status**: `200 OK`

### Delete Ad (Soft)
- **Endpoint**: `GET /Ads/Delete?Id={adId}`  
- **Description**: Soft-delete ad (archived).  
- **Query Parameters**:
  | Name | Type | Required | Description         |
  |------|------|----------|---------------------|
  | `Id` | uuid | Yes      | Ad identifier       |
- **Response** (`Result`): Operation result.
- **Status**: `200 OK`

### Select / Unselect Ad
- **Endpoints**:
  - `POST /Ads/SelectAd`  
  - `POST /Ads/UnselectAd`  
- **Description**: Mark/unmark ad as selected by current user.  
- **Request Body** (`SelectAdCommand` / `UnselectAdCommand`):
  ```json
  { "adId": "uuid" }
  ```
- **Response** (`Result`): Success flag.
- **Status**: `200 OK`

### Featured Ads
- **Get Pricing Options**:
  - **Endpoint**: `GET /Ads/GetPricingOptions`  
  - **Response** (`GetFeaturedPricingQueryResponseResult`): List of `{ durationDays, price, description }`.
- **Feature Ad**:
  - **Endpoint**: `POST /Ads/FeatureAd`  
  - **Request Body** (`FeatureAdCommand`): `{ "adId": "uuid", "durationDays": 7 }`
  - **Response** (`Result`)

### Change Ad Status
- **Endpoint**: `POST /Ads/ChangeAdStatus`  
- **Description**: Override ad lifecycle manually.  
- **Request Body** (`ChangeAdStatusCommand`):
  ```json
  { "adId": "uuid", "newAdStatus": 2 }
  ```
- **Response** (`Result`)

---

## Categories

### Main Categories
- **Create Main Category**: `POST /Categories/create/main-category`  
  - Body: `{ "name": "Vehicles", "parentCategoryId": "uuid" }`  
- **Get All Main Categories**: `GET /Categories/all/main-category`  
  - Query: `?PageNumber=1&PageSize=10&SortBy=name&IsDescending=false`
- **Get By ID**: `GET /Categories/byId/main-category?Id={id}`

### Sub Categories
- **Create Sub Category**: `POST /Categories/create/sub-category`  
  - Body: `{ "name": "Color", "type": "String", "mainCategoryId": "uuid", "options": ["Red","Blue"] }`
- **Get All Sub Categories**: `GET /Categories/all/sub-category?PageNumber=1&PageSize=10`
- **Get By ID**: `GET /Categories/byId/sub-category?Id={id}`

---

## Locations
- **Create Location**: `POST /Locations/Create`  
- **Get All Locations**: `GET /Locations/GetAll`  
- **Get By ID**: `GET /Locations/GetById?Id={id}`  
- **Delete Location**: `POST /Locations/Delete`  

---

## Chat
- **Create Chat Room**: `POST /Chat/CreateChatRoom`  
- **Get Chat Rooms (User)**: `POST /Chat/GetChatRooms`  
- **Get Single Chat Room**: `POST /Chat/GetChatRoom`  
- **Get Messages**: `POST /Chat/GetChatMessages`  
- **Send Message**: `POST /Chat/SendMessage`  
- **Mark Messages As Read**: `POST /Chat/MarkMessagesAsRead`

---

## Profile
- **Get User Data**: `POST /Profile/GetUserData`  
- **Get Active Ads**: `POST /Profile/GetActiveAds`  
- **Get Pending Ads**: `POST /Profile/GetPendingAds`  
- **Get Expired Ads**: `POST /Profile/GetExpiredAds`  
- **Get Rejected Ads**: `POST /Profile/GetRejectedAds`  
- **Get Selected Ads**: `POST /Profile/GetSelectedAds`

---

## Reports
- **Create Report**: `POST /Reports/CreateReport`  
- **Get All Reports**: `GET /Reports/GetAllReports?status={status}`  
- **Get Report By ID**: `GET /Reports/GetReportById/{id}`  
- **Get Reports By Ad ID**: `GET /Reports/GetReportsByAdId/{adId}`  
- **Update Report Status**: `POST /Reports/UpdateReportStatus`

---

## Users
- **Register**: `POST /Users/register`  
- **Update Password**: `POST /Users/update-password`  
- **Change Password (Auth)**: `POST /Users/change-password`

---

## Schemas
All request and response schemas are defined in the `components/schemas` section of the OpenAPI spec. Key objects include:

- `Result` / `StringResult`
- `AdDto`, `AdPreviewDto`
- `CategoryDto`, `MainCategoryDto`, `SubCategoryDto`
- `LocationDto`
- `AppUserDto`
- `ChatRoomDto`, `ChatMessageDto`
- `FeaturedAdPricingDto`
- `GetAllAdsQuery`, `GetAllAdsQueryResponse`

_For full JSON schema definitions and examples, import `swagger.json` into Postman or view in Swagger UI._

