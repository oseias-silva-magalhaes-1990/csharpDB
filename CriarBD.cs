using System;
using System.Text;
using System.Windows.Forms;
using db = System.Data.OleDb;
namespace CriarBD { 

// Definição de um campo public struct DefCampo 
{ 
    public string Nome;
    public string Tipo;
    public bool Null;
    
    public DefCampo( string Nome, string Tipo, bool Null ) 
    { 
        this.Nome = Nome;
        this.Tipo = Tipo;
        this.Null = Null;
    } 
} 
// Cria fisicamente uma nova base de dados 
    public class DefinirBanco 
    { 
        // Armazena a string conexão com o banco 
        public string StringConexao;

        // Cria fisicamente o banco de dados 
        public virtual bool CriarDatabase( ) 
        { 
        // Aqui ocorre interação com código legado 
        // também conhecido como "não-gerenciado" 
        ADOX.Catalog Banco = new ADOX.Catalog( );
        
        // Neste método, optamos por deixar o tratamento 
        // da exceção por conta de quem disparou a ação 
        Banco.Create( StringConexao );
        return true;
    }

