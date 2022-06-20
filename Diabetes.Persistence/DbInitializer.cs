using System;
using System.Collections.Generic;
using System.Linq;
using Diabetes.Domain;
using Diabetes.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(DataDbContext context)
        {
            context.Database.EnsureCreated();

            var items = new List<Food>
            {
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Хлеб белый пшеничный",
            		Carbohydrate = 47.54,
            		Fat = 4.53,
            		Kcal = 274,
            		Protein = 10.67,
            		Details = "Простой домашний пшеничный хлеб"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Хлеб из овсяных отрубей",
            		Carbohydrate = 39.8,
            		Fat = 4.4,
            		Kcal = 236,
            		Protein = 10.4,
            		Details = "Диетический хлеб"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Хлеб из пророщенной пшеницы",
            		Carbohydrate = 33.9,
            		Fat = 0,
            		Kcal = 188,
            		Protein = 13.16,
            		Details = "Описание отсутствует"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Хлеб из пшеничных отрубей",
            		Carbohydrate = 47.8,
            		Fat = 3.4,
            		Kcal = 248,
            		Protein = 8.8,
            		Details = "Вкусный белковый хлеб"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Хлеб из рисовых отрубей",
            		Carbohydrate = 43.5,
            		Fat = 4.6,
            		Kcal = 243,
            		Protein = 8.9,
            		Details = "Описание отсутствует"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Хлеб мультизерновой",
            		Carbohydrate = 43.3,
            		Fat = 4.23,
            		Kcal = 265,
            		Protein = 13.36,
            		Details = "Описание отсутствует"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Хлеб овсяный",
            		Carbohydrate = 48.5,
            		Fat = 4.4,
            		Kcal = 269,
            		Protein = 8.4,
            		Details = "Просто и быстро приготовить"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Хлеб пшеничный цельнозерновой",
            		Carbohydrate = 42.7,
            		Fat = 3.5,
            		Kcal = 252,
            		Protein = 12.45,
            		Details = "Вид хлеба, изготовленный из муки, которая частично или полностью измельчена из цельного или почти цельного зерна пшеницы"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Хлеб ржаной",
            		Carbohydrate = 48.3,
            		Fat = 3.3,
            		Kcal = 259,
            		Protein = 8.5,
            		Details = "Хлеб из ржаной муки"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Хлебцы мультизерновые",
            		Carbohydrate = 66.3,
            		Fat = 15.84,
            		Kcal = 453,
            		Protein = 11.25,
            		Details = "Идеально подходят в качестве закуски в любое время"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Хлебцы ржаные",
            		Carbohydrate = 82.2,
            		Fat = 1.3,
            		Kcal = 366,
            		Protein = 7.9,
            		Details = "Вкусные, приятные и полезные ржаные сухарики, которые могут стать отличным дополнением к завтраку, обеду, или ужину"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Булочки из сладкого дрожжевого теста",
            		Carbohydrate = 56.4,
            		Fat = 11.58,
            		Kcal = 367,
            		Protein = 9.42,
            		Details = "Бесподобные сладкие булочки из дрожжевого теста, аромат которых не оставит никого равнодушным"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Лаваш",
            		Carbohydrate = 55.7,
            		Fat = 1.2,
            		Kcal = 275,
            		Protein = 9.1,
            		Details = "Пресный белый хлеб в виде тонкой лепёшки из пшеничной муки"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Пита",
            		Carbohydrate = 55.7,
            		Fat = 1.2,
            		Kcal = 275,
            		Protein = 9.1,
            		Details = "Небольшое описание"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Пончики",
            		Carbohydrate = 47.1,
            		Fat = 24.93,
            		Kcal = 434,
            		Protein = 5.31,
            		Details = "Круглый, плоский хлеб, который выпекают как из обойной муки, так и из пшеничной муки высшего сорта"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Сухари из хлеба",
            		Carbohydrate = 66.36,
            		Fat = 15.14,
            		Kcal = 451,
            		Protein = 12.34,
            		Details = "Это быстрое и несложное в приготовлении блюдо"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Сухари панировочные",
            		Carbohydrate = 71.98,
            		Fat = 5.3,
            		Kcal = 395,
            		Protein = 13.35,
            		Details = "Сухарная крошка из пшеничного хлеба, используемая для покрытия обжариваемых кулинарных изделий из мяса, рыбы и овощей"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Тортилья",
            		Carbohydrate = 63.49,
            		Fat = 21.79,
            		Kcal = 476,
            		Protein = 6.41,
            		Details = "Тонкая лепёшка из кукурузной или пшеничной муки"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Жевательная резинка",
            		Carbohydrate = 96.7,
            		Fat = 0.3,
            		Kcal = 360,
            		Protein = 0,
            		Details = "Кулинарное изделие, которое состоит из несъедобной эластичной основы и различных вкусовых и ароматических добавок"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Кекс шоколадный",
            		Carbohydrate = 52.84,
            		Fat = 20.05,
            		Kcal = 389,
            		Protein = 3.48,
            		Details = "Разновидность шоколадной выпечки"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Крекер",
            		Carbohydrate = 70.73,
            		Fat = 16.4,
            		Kcal = 455,
            		Protein = 7.3,
            		Details = "Хрустящее печенье со слоистой структурой и маслянистой поверхностью"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Крендельки",
            		Carbohydrate = 79.2,
            		Fat = 3.5,
            		Kcal = 381,
            		Protein = 9.1,
            		Details = "Крендельки готовятся из дрожжевого теста на молоке или воде"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Мармелад",
            		Carbohydrate = 69.95,
            		Fat = 0.02,
            		Kcal = 266,
            		Protein = 0.15,
            		Details = "Представляет собой сгущенный сок и мякоть фруктов (яблок, айвы, апельсинов)"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Мёд",
            		Carbohydrate = 82.4,
            		Fat = 0,
            		Kcal = 304,
            		Protein = 0.3,
            		Details = "Сладкий, вязкий продукт, который вырабатывают пчёлы и родственные насекомые"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Мороженое",
            		Carbohydrate = 23.6,
            		Fat = 11,
            		Kcal = 207,
            		Protein = 3.5,
            		Details = "Очень древнее лакомство, без которого не обойтись в жару"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Печенье овсяное",
            		Carbohydrate = 68.7,
            		Fat = 18.1,
            		Kcal = 450,
            		Protein = 6.2,
            		Details = "Один из самых популярных видов домашнего печенья"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Халва",
            		Carbohydrate = 60.49,
            		Fat = 21.52,
            		Kcal = 469,
            		Protein = 12.49,
            		Details = "Небольшое описание"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Лапша варёная",
            		Carbohydrate = 30.86,
            		Fat = 0.93,
            		Kcal = 158,
            		Protein = 5.8,
            		Details = "Простое в приготовлении блюдо"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Лапша в сухом виде",
            		Carbohydrate = 74.64,
            		Fat = 1.51,
            		Kcal = 371,
            		Protein = 13.04,
            		Details = "Можно есть и в сухом виде"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Лапша гречневая варёная",
            		Carbohydrate = 21.44,
            		Fat = 0.1,
            		Kcal = 99,
            		Protein = 5.06,
            		Details = "Продукт идеально подходящий для диеты"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Лапша гречневая в сухом виде",
            		Carbohydrate = 74.62,
            		Fat = 0.71,
            		Kcal = 336,
            		Protein = 14.38,
            		Details = "Гречневая лапша, которая не была приготовлена"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Яйцо куриное варёное",
            		Carbohydrate = 1.12,
            		Fat = 10.61,
            		Kcal = 155,
            		Protein = 12.58,
            		Details = "Продукт полезный и доступный, известный с давних пор и востребованный в наше время в ежедневном рационе"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Яйцо куриное жареное",
            		Carbohydrate = 0.83,
            		Fat = 14.84,
            		Kcal = 196,
            		Protein = 13.61,
            		Details = "Куриное яйцо, которые было пожарено"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Сыр Пармезан",
            		Carbohydrate = 3.22,
            		Fat = 25.83,
            		Kcal = 392,
            		Protein = 35.75,
            		Details = "Итальянский сорт твёрдого сыра долгого созревания. Текстура ломкая, сыр с неровным срезом, крошится при нарезании. Вкус нежный, с пикантным послевкусием"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Сыр плавленный",
            		Carbohydrate = 3.5,
            		Fat = 28.6,
            		Kcal = 295,
            		Protein = 7.1,
            		Details = "Молочный продукт, получаемый в результате переработки обычного сыра или творога"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Сыр Моцарелла",
            		Carbohydrate = 5.58,
            		Fat = 19.78,
            		Kcal = 295,
            		Protein = 23.75,
            		Details = "Относится к семейству пасты филата, что в переводе означает «тянутый сыр»"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Масло сливочное",
            		Carbohydrate = 0,
            		Fat = 99.48,
            		Kcal = 876,
            		Protein = 0.28,
            		Details = "Популярный продукт, который изготавливается на основе молочных ингредиентов — сливок и других продуктов переработки молока"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Сметана",
            		Carbohydrate = 7.1,
            		Fat = 10.6,
            		Kcal = 136,
            		Protein = 3.5,
            		Details = "Исконно русский молочный продукт, приготавливаемый из сливок с последующим молочнокислым брожением"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Кефир",
            		Carbohydrate = 4.48,
            		Fat = 0.93,
            		Kcal = 41,
            		Protein = 3.79,
            		Details = "Кисломолочный напиток, получаемый из цельного или обезжиренного коровьего молока"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Творог 2% жирности",
            		Carbohydrate = 4.76,
            		Fat = 2.27,
            		Kcal = 81,
            		Protein = 10.45,
            		Details = "Кисломолочный продукт, получаемый в результате сквашивания молока. Имеет 2% жирности"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Йогурт",
            		Carbohydrate = 12.29,
            		Fat = 3,
            		Kcal = 106,
            		Protein = 7.33,
            		Details = "Популярнейший молочный десерт, соединяющий в себе отличный вкус и лекарственные свойства"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Чай чёрный без сахара",
            		Carbohydrate = 0,
            		Fat = 0,
            		Kcal = 0,
            		Protein = 0,
            		Details = "Наиболее популярный вид чая в Европе"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Чай чёрный с сахаром и лимоном",
            		Carbohydrate = 10.8,
            		Fat = 0.22,
            		Kcal = 45,
            		Protein = 0,
            		Details = "Наиболее популярный вид чая в Европе, но с добавлением сахара и лимона"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Кофе с молоком и сахаром",
            		Carbohydrate = 12.6,
            		Fat = 1.38,
            		Kcal = 71,
            		Protein = 1.98,
            		Details = "В сочетании с сахаром и молоком чёрный кофе действует иначе, мягче и медленнее"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Морской чёрт сырой",
            		Carbohydrate = 0,
            		Fat = 1.52,
            		Kcal = 76,
            		Protein = 14.48,
            		Details = "Рыба богатая витаминами и минералами"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Яблоко",
            		Carbohydrate = 13.81,
            		Fat = 0.17,
            		Kcal = 52,
            		Protein = 0.26,
            		Details = "Сочный плод яблони, который употребляется в пищу в свежем виде"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Яблоко Голден",
            		Carbohydrate = 13.6,
            		Fat = 0.15,
            		Kcal = 57,
            		Protein = 0.28,
            		Details = "Сорт яблок жёлтого цвета, один из символов Западной Виргинии"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Клубника замороженная",
            		Carbohydrate = 9.13,
            		Fat = 0.11,
            		Kcal = 35,
            		Protein = 0.43,
            		Details = "Превосходный низкокалорийный десерт полезный для здоровья, который была заморожена"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Клубника свежая",
            		Carbohydrate = 7.68,
            		Fat = 0.3,
            		Kcal = 32,
            		Protein = 0.67,
            		Details = "Превосходный низкокалорийный десерт полезный для здоровья, один из лучших источников витамина С"
            	},
            	
            	new Food
            	{
            		Id = Guid.NewGuid(),
            		Name = "Вишня свежая",
            		Carbohydrate = 12.18,
            		Fat = 0.3,
            		Kcal = 50,
            		Protein = 1,
            		Details = "Вишня отличается прекрасными пищевыми качествами плодов, рано созревает, дает высокий урожай"
            	}
            }; 

            if (!context.Foods.Any())
            {
                context.Foods.AddRange(items);
                context.SaveChanges();
            }
        }
    }
}