## Barcode-Reading-Server-ASP-NET-Core

A barcode reading server demo in ASP.NET Core for [Dynamsoft Barcode Reader .NET](https://www.dynamsoft.com/barcode-reader/docs/server/programming/dotnet/user-guide.html).

Run the app:

```bash
dotnet run
```

## Web API Usage

* Endpoint: `/readBarcodes`

* Request: 

   Headers:
   
   ```json
   {
     "Content-Type":"application/json"
   }
   ```

   Body:
   
   ```json
   {
     "base64": "the base64 string of the image for decoding"
   }
   ```

* Example response:

```json
{
  "elapsedTime": 0,
  "results": [
    {
      "barcodeText":"",
      "barcodeFormat":"",
      "x1":0,
      "x2":0,
      "x3":0,
      "x4":0,
      "y1":0,
      "y2":0,
      "y3":0,
      "y4":0
    }
  ]
}
```