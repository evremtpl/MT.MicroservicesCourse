namespace MT.FreeCourse.Services.Basket.Dtos
{
    public class BasketDto
    {
        public string UserId  { get; set; }
        public string DiscountCode { get; set; }
        public string DiscountCode1 { get; set; }
        public List<BasketItemDto> basketItems { get; set; }

        public decimal TotalPrice
        {
            get => basketItems.Sum(x=>x.Price *x.Quantity);
        }
    }
}
