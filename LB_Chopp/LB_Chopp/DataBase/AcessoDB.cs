using LB_Chopp.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LB_Chopp.DataBase
{
    public class AcessoDB
    {
        readonly LiteDatabase _litedb;
        public AcessoDB(string dbPath)
        {
            _litedb = new LiteDatabase(Path.Combine(dbPath, "Banco.db"));
            _litedb
                .Mapper
                .Entity<Barril>()
                .Id(p=> p.Id_barril);
            _litedb
                .Mapper
                .Entity<BarrisTipo>()
                .Id(p => p.ID);
            _litedb
                .Mapper
                .Entity<Chopeira>()
                .Id(p => p.Id_chopeira);
            _litedb
                .Mapper
                .Entity<ChopeiraDisponivel>()
                .Id(p => p.ID);
            _litedb
                .Mapper
                .Entity<Chopp>()
                .Id(p => p.Cd_produto);
            _litedb
                .Mapper
                .Entity<Cilindro>()
                .Id(p => p.Id_cilindro);
            _litedb
                .Mapper
                .Entity<Cliente>()
                .Id(p => p.Cd_clifor);
            _litedb
                .Mapper
                .Entity<ReservaBarril>()
                .Id(p => p.ID);
            _litedb
                .Mapper
                .Entity<ReservaChopeira>()
                .Id(p => p.ID);
            _litedb
                .Mapper
                .Entity<ReservaChopp>()
                .Id(p => p.ID);
            _litedb
                .Mapper
                .Entity<ReservaCilindro>()
                .Id(p => p.ID);
            _litedb
                .Mapper
                .Entity<ReservaFoto>()
                .Id(p => p.ID);
        }
        public void ClearBancoDados()
        {
            _litedb.BeginTrans();
            try
            {
                _litedb.GetCollection<Barril>().DeleteAll();
                _litedb.GetCollection<BarrisTipo>().DeleteAll();
                _litedb.GetCollection<Chopeira>().DeleteAll();
                _litedb.GetCollection<ChopeiraDisponivel>().DeleteAll();
                _litedb.GetCollection<Chopp>().DeleteAll();
                _litedb.GetCollection<Cilindro>().DeleteAll();
                _litedb.GetCollection<ReservaBarril>().DeleteAll();
                _litedb.GetCollection<ReservaChopeira>().DeleteAll();
                _litedb.GetCollection<ReservaChopp>().DeleteAll();
                _litedb.GetCollection<ReservaCilindro>().DeleteAll();
                _litedb.GetCollection<ReservaFoto>().DeleteAll();
                _litedb.Commit();
            }
            catch { _litedb.Rollback(); }
        }

        #region Barril
        public async Task<IEnumerable<Barril>>
        #endregion
    }
}
