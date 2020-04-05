namespace GamesHub
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell(); 
            XF.Material.Forms.Material.Init(this, "Material.Configuration");
        }
    }
}