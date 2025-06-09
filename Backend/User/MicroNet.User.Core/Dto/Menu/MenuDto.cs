namespace MicroNet.User.Core.Dto.Menu
{
    public class MenuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public int MenuOrderId { get; set; }
        public List<SubMenuDto> SubMenuDto { get; set; } = new();
    }

    public class SubMenuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public Guid? MenuId { get; set; }
    }
}
