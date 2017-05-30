using Microsoft.WindowsAzure.MobileServices;
using ProvaEvidencia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaEvidencia.Models
{
    public class CategoriaItemManager
    {
        public MobileServiceClient client;
        public IMobileServiceTable<Categoria> todoTable;

        public CategoriaItemManager()
        {
            this.client = new MobileServiceClient(Constant.ApplicationURL);

            this.todoTable = client.GetTable<Categoria>();

        }

        public async Task SaveTaskAsync(Categoria item)
        {
           

                if (item.ID == null)
                {
                    await todoTable.InsertAsync(item);
                }
                else
                {
                    await todoTable.UpdateAsync(item);
                }
           
        }

        public async Task RemoveAsync(Categoria item)
        {
            if (item.ID == null)
            {
                return;
            }
            else
            {
              await  todoTable.DeleteAsync(item);
            }
        }
        
    }
}
