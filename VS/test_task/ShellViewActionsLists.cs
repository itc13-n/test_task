using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_task.Views
{
    public static class ShellViewActionsLists
    {
        public static readonly string[] contextActionsManager = { "Указать клиента", "Список клиентов" };
        public static readonly string[] contextActionsClient =  { "Выделить менеджера", "Смотреть контакт", "Список товаров" } ;
        public static readonly string[] contextActionsProduct = { "Отдать клиенту", "Список клиентов" };
        public static readonly string[] mainActions = new string[] { "Показать менеджеров", "Показать клиентов", "Показать товары" };
        public static readonly string[] changeItemActions = new string[] { "Добавить", "Удалить", "Редактировать" };

    }
}
