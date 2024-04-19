# ImageManipulation

APIs for Image manipulation (Add/Update/Delete/GetALL)

## Tech Stack

- .Net core APIs (.NET 8)
- .EF Core 8
- MSSQL

## ImagePath

eg:
`BaseUrl/resources/image_name.extension`

## Endpoints

- GET: api/products
  RESPONSE:

  ```
   //response

    [{"id":1,"productName":"Product 1","productImage":"5b7c9e4d-3324-4f71-9b4c-a938d7edafde.jpg","imageFile":null},{"id":2,"productName":"Product 2","productImage":"347c5bf5-df20-4b30-ad63-677c829820d2.jpg","imageFile":null}]
  ```

- GET : api/products/1
  RESPONSE: 200

  ```
    {
      "id": 3,
      "productName": "Product 31",
      "productImage": "4944dbdb-b3ec-49d8-81ba-c870d0edcf0b.jpg"
    }
  ```

- POST: api/products
  Content Type : Form Data
  Body:

  ```
   ProductName : STRING | REQUIRED | MaxLength(30)
   ImageFile: FILE | REQUIRED | MaxLength: 1 MB
  ```

  Constraints:
  `ImageFile` Allowed file types .jpg, .jpeg, .png

  RESPONSE: 201 created

  ```
    {
      "id": 1,
      "productName": "Product 1",
      "productImage": "989f0a66-f65c-49d1-8e23-2f9ca24b9ecb.jpg"
    }
  ```

  - PUT: api/products/ {id:INTEGER}
    Content Type : Form Data
    Body:

  ```
   Id: INTEGER | REQUIRED
   ProductName : STRING | REQUIRED | MaxLength(30)
   ImageFile: FILE | NULL,
   ProductImage : STRING | REQUIRED | MaxLength(50)
  ```

  Constraints:
  `ImageFile` Allowed file types .jpg, .jpeg, .png

  RESPONSE: 200
  {
  "id": 3,
  "productName": "Product 31",
  "productImage": "4944dbdb-b3ec-49d8-81ba-c870d0edcf0b.jpg"
  }

- DELETE: api/products/{id:INTEGER}
  RESPONSE: On success returns 204 Empty

I am assuming that, you have already installed Visual Studio 2022 (It is the latest as of march,2024) and MS SQL Server Management Studio (I am using mssql server 2022 as of march,2024). Now, follow the following steps.

## How to run this Project?

- Open command prompt. Go to a directory where you want to clone this project. Use this command to clone the project.
  `git clone https://github.com/rd003/ImageManipulation_APIs_DotNetCore`
  
- Go to the directory where you have cloned this project, open the directory `BookShoppingCart-Mvc`. You will find a file with name `BookShoppingCartMvc.sln`. Double click on this file and this project will be opened in Visual Studio.

- Open `appsettings.json` file and update connection string
  "ConnectionStrings": {
    "conn": "data source=your_server_name;initial catalog=MovieStoreMvc; integrated security=true;encrypt=false"
  }

- Delete Migrations folder

- Open `Tools > Package Manager > Package manager console`.

  Run these 2 commands
  
  ```
    (i) add-migration init
    (ii) update-database
  ```

- Now you can run this project

Thank's for ⭐ 😅
