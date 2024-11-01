using src.Entity;

namespace src.Utils
{
    public class FilterOptions
    {
        public decimal? MinPrice { get; set; }=0;
        public decimal? MaxPrice { get; set; }=10000;
        public Category? Category { get; set; }
    }
}
