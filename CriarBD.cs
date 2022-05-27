//Fragmento do Livro
//C# E .NET - GUIA DO DESENVOLVEDOR

using System;
using System.Text;
using System.Windows.Forms;
using db = System.Data.OleDb;
namespace CriarBD 
{ 

    // Definição de um campo 
    public struct DefCampo 
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

        // Criar fisicamente a tabela 
        public virtual bool CriarTabela( string NomeTabela, DefCampo[ ] Campos) 
        { 
            // Preferimos usar um StringBuilder neste método 
            // devido às intensas modificações até chegar à 
            // string final.
 
            // A string é criada com limite máximo de 2k 
            StringBuilder ComandoSQL = new StringBuilder( "CREATE TABLE " + NomeTabela + " ( ", 2048 );

            // Aqui ocorre um loop que vai reunir todas as 
            // definições dos campos para compor um comando 
            // CREATE TABLE adequado 
            foreach( DefCampo Campo in Campos ) 
            { 
                ComandoSQL.Append( Campo.Nome + " " + Campo.Tipo );

                if ( Campo.Null ) 
                     ComandoSQL.Append( " NULL, " );
                else 
                     ComandoSQL.Append( " NOT NULL, " );
        } 

            // Remove a última vírgula e espaço que sobraram 
            ComandoSQL.Remove( ComandoSQL.Length - 2, 2 );

            // Fecha parênteses 
            ComandoSQL.Append( " ) " );

            // Executa a tabela 
            ExecutarComando( ComandoSQL.ToString( ) );
      
            return true;
        } 

        // Executar uma sentença SQL no banco 
        public virtual bool ExecutarComando( string ComandoSQL ) 
        { 
            // Aqui estamos usando ADO.NET 
            db.OleDbConnection cn = new db.OleDbConnection( StringConexao );
            db.OleDbCommand cmd = new db.OleDbCommand( ComandoSQL, cn );
    
            // Neste método, optamos por tratamento de 
            // exceções in-loco 
            try 
            { 
                cn.Open( );
                cmd.ExecuteNonQuery( );
                cn.Close( );
                return true;
            } 
            catch( Exception e ) 
            { 
                MessageBox.Show( e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return false;
            } 
        }
    }
}
