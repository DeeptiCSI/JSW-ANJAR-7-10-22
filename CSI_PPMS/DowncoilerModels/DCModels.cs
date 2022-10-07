namespace CSI_PPMS.DowncoilerModels
{
    public class DCModels
    {
    }


    public class DownCoilerWeightResponseModel
    {
        public string AWEIGHT { get; set; }
        public string ZIND { get; set; }
        public RETURN1 RETURN1 { get; set; }
    }

    public class RETURN1
    {
        public item Item { get; set; }
    }

    public class item
    {
        public string TYPE { get; set; }
        public string CODE { get; set; }
        public string MESSAGE { get; set; }
        public string LOG_NO { get; set; }
        public string LOG_MSG_NO { get; set; }
        public string MESSAGE_V1 { get; set; }
        public string MESSAGE_V2 { get; set; }
        public string MESSAGE_V3 { get; set; }
        public string MESSAGE_V4 { get; set; }
    }



    public class DCWeightResponse
    {
        public string MatId { get; set; }
        public string CoilId { get; set; }
    }
}
