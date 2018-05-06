using SimpleJSON;

namespace AbrStudioSdk.Iab.Data
{
    public class CodeMessagePair
    {
        public int Code { get; set; }
        public string Message { get; set; }
        
        public CodeMessagePair(JSONNode jsonNode)
        {
            Code = jsonNode[Constants.CodeKey].AsInt;
            Message = jsonNode[Constants.MessageKey].Value;
        }
    }
}