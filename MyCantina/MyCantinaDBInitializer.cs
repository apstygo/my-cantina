using System.Collections.Generic;
using System.Data.Entity;

namespace MyCantina
{
    class MyCantinaDbInitializer : CreateDatabaseIfNotExists<MyCantinaDbContext>
    {
        protected override void Seed(MyCantinaDbContext context)
        {
            IList<Cantina> cantinas = new List<Cantina>();
            cantinas.Add(new Cantina("На Кирпичной", "ул. Кирпичная, д. 33"));
            cantinas.Add(new Cantina("На Шаболовке", "ул. Шаболовка, д. 26"));
            cantinas.Add(new Cantina("На Мясницкой", "ул. Мясницкая, д. 20"));
            foreach (Cantina cantina in cantinas)
            {
                context.Cantinas.Add(cantina);
            }

            context.Dishes.Add(new Dish("Чай", 20, 1));
            context.Dishes.Add(new Dish("Кофе", 120, 1));
            context.Dishes.Add(new Dish("Хлеб", 10, 1));
            context.Dishes.Add(new Dish("Борщ", 70, 2));
            context.Dishes.Add(new Dish("Плов", 150, 2));
            context.Dishes.Add(new Dish("Круассан", 40, 2));
            context.Dishes.Add(new Dish(@"Салат 'Весне я рад'", 50, 2));
            context.Dishes.Add(new Dish("Пампушки", 30, 3));
            context.Dishes.Add(new Dish("Котлеты с пюрешкой", 100, 3));
            context.Dishes.Add(new Dish("Макароны по-флотски", 90, 3));
            
            base.Seed(context);

            Logger.Log("Создана и заполнена новая база данных");
        }
    }
}
