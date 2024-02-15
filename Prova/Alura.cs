using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using Prova.Domain.Entities;
using Prova.Domain.Interface;


namespace Prova
{
    public class Alura
    {
        public IWebDriver driver;
        Curso curso = new Curso();
        private readonly ICursoService cursoServices;

        public Alura()
        {
            driver = new ChromeDriver("C:\\chromedriver-win64\\chromedriver.exe");

        }

        public void Login()
        {
            try
            {
                driver.Navigate().GoToUrl("http://www.alura.com.br/");

                driver.FindElement(By.Id("header-barraBusca-form-campoBusca")).SendKeys("RPA");
                driver.FindElement(By.ClassName("header__nav--busca-submit")).Click();

                BuscarCurso("RPA");

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Environment.Exit(0);
            }
        }

        public void BuscarCurso(string nomeCurso)
        {
            try
            {
                if (driver.FindElements(By.XPath("//*[@id=\"busca-resultados\"]")).Count() > 1)
                {
                    Console.WriteLine("Não foi encontrado resultado ou pagina deva ter ocorrido mudança para manutenção.");
                    Environment.Exit(0);
                }

                var validarNome = driver.FindElements(By.ClassName("busca-resultado-nome"));

                foreach (var nome in validarNome)
                {
                    if (nome.Text.Contains(nomeCurso))
                    {
                        curso.Titulo = nome.Text;
                        string Descricao = driver.FindElement(By.ClassName("busca-resultado-descricao")).Text;
                        curso.Descricao = Descricao;
                        driver.FindElement(By.ClassName("busca-resultado-descricao")).Click();

                        Thread.Sleep(1000);

                        ConteudoCurso();
                        BuscarProfessor();
                        driver.Navigate().Back();
                        Thread.Sleep(1000);
                        driver.Navigate().Back();
                        Thread.Sleep(3000);
                    }
                }



                cursoServices.Insert(curso);

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

        }

        public void ConteudoCurso()
        {
            try
            {
                var validarConteudo = driver.FindElements(By.XPath("/html/body/section[1]/div/div[2]/div[1]")).Count;

                if (validarConteudo == 0)
                {
                    Console.WriteLine("Não existe conteudo");
                }

                var cargaHoraria = driver.FindElements(By.XPath("/html/body/section[1]/div/div[2]/div[1]/div/div[1]/div/p[1]"));

                if (cargaHoraria.Count() > 0)
                {
                    string horario = driver.FindElement(By.XPath("/html/body/section[1]/div/div[2]/div[1]/div/div[1]/div/p[1]")).Text.Trim();
                    curso.CargaHoraria = horario;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + "*** Incidente ConteudoCurso");
            }
        }

        public void BuscarProfessor()
        {
            try
            {
                var validarConteudoRelacional = driver.FindElements(By.XPath("/html/body/section[4]/section[2]/div"));

                if (validarConteudoRelacional.Count < 0)
                {
                    Console.WriteLine("Não contem professor ou cursos relacionado para extrair");
                }

                var scroll = driver.FindElement(By.XPath("/html/body/section[4]/section[2]/ul"));

                Actions actions = new Actions(driver);
                actions.MoveToElement(scroll);
                actions.Perform();

                driver.FindElement(By.ClassName("lista-guides__link")).Click();
                var validaListaProfessor = driver.FindElements(By.XPath("//*[@id=\"instrutores\"]/div/ul"));

                if (validaListaProfessor.Count > 0)
                {
                    var scroll2 = driver.FindElement(By.XPath("//*[@id=\"instrutores\"]/div/ul"));

                    actions = new Actions(driver);
                    actions.MoveToElement(scroll2);
                    actions.Perform();

                    var Lstprofessor = driver.FindElements(By.ClassName("formacao-instrutor-nome"));

                    string nomeProfessor = "";

                    foreach (var item in Lstprofessor)
                    {

                        if (nomeProfessor != item.Text)
                        {
                            if (curso.Professor != null)
                            {
                                curso.Professor = curso.Professor + " - " + item.Text;
                            }
                            else
                            {
                                curso.Professor = item.Text;
                            }

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + "*** incidente BuscarProfessor");
            }
        }

    }
}
