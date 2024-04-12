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

  ```
   //response

    [{"id":1,"productName":"Product 1","productImage":"5b7c9e4d-3324-4f71-9b4c-a938d7edafde.jpg","imageFile":null},{"id":2,"productName":"Product 2","productImage":"347c5bf5-df20-4b30-ad63-677c829820d2.jpg","imageFile":null}]
  ```

- POST: api/products
  Content Type : Form Data
  Body:

  ```
   ProductName : STRING | REQUIRED | MaxLength(30)
   ImageFile: FILE | REQUIRED | MaxLength: 1 MB
  ```

  Constraints:
  Allowed file types .jpg, .jpeg, .png

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
  Allowed file types .jpg, .jpeg, .png

- DELETE: api/products/{id:INTEGER}
  On success returns 204 Empty
