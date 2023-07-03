using Dynamsoft;
using Dynamsoft.DBR;
string errorMsg;
EnumErrorCode errorCode = BarcodeReader.InitLicense("DLS2eyJoYW5kc2hha2VDb2RlIjoiMjAwMDAxLTE2NDk4Mjk3OTI2MzUiLCJvcmdhbml6YXRpb25JRCI6IjIwMDAwMSIsInNlc3Npb25QYXNzd29yZCI6IndTcGR6Vm05WDJrcEQ5YUoifQ==", out errorMsg);
if (errorCode != EnumErrorCode.DBR_SUCCESS)
{
    // Add your code for license error processing;
    Console.WriteLine(errorMsg);
}
BarcodeReader reader = new BarcodeReader();
try
{
    TextResult[] results = reader.DecodeFile(@"AllSupportedBarcodeTypes.png", "");
    if (results != null && results.Length > 0)
    {
        for (int i = 0; i < results.Length; ++i)
        {
            Console.WriteLine((i + 1) + "." + results[i].BarcodeFormatString + ": " + results[i].BarcodeText);
        }
    } 
}
catch (BarcodeReaderException exp)
{
    Console.WriteLine(exp.Message);
}
