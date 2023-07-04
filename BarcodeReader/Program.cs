using Dynamsoft;
using Dynamsoft.DBR;

initLicense();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

var app = builder.Build();
app.UseStaticFiles();
// Shows UseCors with CorsPolicyBuilder.
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.MapPost("/readBarcodes", (Image image) => {
    BarcodeReader reader = new BarcodeReader();
    long startTime = getUnixTimestamp();
    TextResult[] results = reader.DecodeBase64String(image.base64,"");
    long elapsedTime = getUnixTimestamp() - startTime;
    List<BarcodeResult> barcodeResults = wrappedResults(results);
    DecodingResponse response = new DecodingResponse(
      barcodeResults,elapsedTime
    );
    return response;
});

app.Run();

long getUnixTimestamp(){
    DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
    return dto.ToUnixTimeMilliseconds();
}

void initLicense() {
    string errorMsg;
    EnumErrorCode errorCode = BarcodeReader.InitLicense("DLS2eyJoYW5kc2hha2VDb2RlIjoiMjAwMDAxLTE2NDk4Mjk3OTI2MzUiLCJvcmdhbml6YXRpb25JRCI6IjIwMDAwMSIsInNlc3Npb25QYXNzd29yZCI6IndTcGR6Vm05WDJrcEQ5YUoifQ==", out errorMsg);
    
    if (errorCode != EnumErrorCode.DBR_SUCCESS)
    {
        // Add your code for license error processing;
        Console.WriteLine(errorMsg);
    }
}

List<BarcodeResult> wrappedResults(TextResult[] results) {
    List<BarcodeResult> barcodeResults = new List<BarcodeResult>();
    foreach (var result in results)
    {
      BarcodeResult barcodeResult = new BarcodeResult(
        barcodeText: result.BarcodeText,
        barcodeFormat: result.BarcodeFormatString,
        x1: result.LocalizationResult.ResultPoints[0].X,
        x2: result.LocalizationResult.ResultPoints[1].X,
        x3: result.LocalizationResult.ResultPoints[2].X,
        x4: result.LocalizationResult.ResultPoints[3].X,
        y1: result.LocalizationResult.ResultPoints[0].Y,
        y2: result.LocalizationResult.ResultPoints[1].Y,
        y3: result.LocalizationResult.ResultPoints[2].Y,
        y4: result.LocalizationResult.ResultPoints[3].Y
      );
      barcodeResults.Add(barcodeResult);
    }
    return barcodeResults;
}

record Image(string base64);
record DecodingResponse(
  List<BarcodeResult> results,
  long elapsedTime
);
record BarcodeResult(
  string barcodeText,
  string barcodeFormat,
  int x1,
  int x2,
  int x3,
  int x4,
  int y1,
  int y2,
  int y3,
  int y4
);

