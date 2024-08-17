namespace Model.Custom.Rahkaran
{
    public class CostCenterListDto
    {
        public long CostCenterID { get; set; }
        public int? CompanyRef { get; set; }
        public string Name { get; set; } = null!;
    }
}
