namespace DevFreela.Application.InputModels
{
    public class NewProjectInputModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int idCliente { get; set; }
        public int idFreelancer { get; set; }
        public decimal TotalCoast { get; set; }
    }
}
