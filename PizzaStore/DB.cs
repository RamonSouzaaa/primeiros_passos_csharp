namespace PizzaStore.DB
{
    public record PizzaR {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class PizzaDB {
        private static List<PizzaR> pizzas = new List<PizzaR>();

        public static List<PizzaR> GetPizzas() {
            return pizzas;
        }

        public static PizzaR GetPizza(int id) {
            return pizzas.Find(item => item.Id == id);
        }

        public static void Add(PizzaR pizza) {
            pizzas.Add(pizza);
        }

        public static PizzaR Update(PizzaR pizza) {
            pizzas.ForEach(item => {
                if(item.Id == pizza.Id) {
                    item.Name = pizza.Name;
                }
            });

            return pizza;
        }

        public static void Delete(int id) {
            pizzas = pizzas.FindAll(item => item.Id != id).ToList();
        }
    }
}