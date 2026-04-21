using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static Demo1_LINQ.ListGenerator;

namespace Demo1_LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region 1- Filtering (Where)

            //var outOfStockProducts = Products
            //    .Where(p => p.UnitsInStock == 0);

            //Console.WriteLine("Out Of Stock Products:");
            //foreach (var item in outOfStockProducts)
            //{
            //    Console.WriteLine(item);
            //}

            /*
             الشرح:
             Where = فلترة البيانات
             هنا بنجيب كل المنتجات اللي المخزون بتاعها = 0
             زي if لكن بشكل مختصر وأنضف
            */

            #endregion


            #region 2- Transformation (Select / SelectMany)

            //var productNames = Products
            //    .Select(p => p.ProductName);

            //Console.WriteLine("\nProduct Names:");
            //foreach (var name in productNames)
            //{
            //    Console.WriteLine(name);
            //}

            //var allOrders = Customers
            //    .SelectMany(c => c.Orders);

            /*
             الشرح:
             Select = تحويل البيانات (اختيار جزء منها)
             هنا بنرجع اسم المنتج بس بدل المنتج كله

             SelectMany = دمج Lists جوا بعض
             يعني كل Orders من كل Customers في List واحدة
            */

            #endregion


            #region 3- Anonymous Object

            //var productInfo = Products
            //    .Select(p => new
            //    {
            //        p.ProductID,
            //        p.ProductName,
            //        p.UnitPrice
            //    });

            /*
             الشرح:
             Anonymous Object = object بدون class
             بنستخدمه لما نحتاج نرجع بيانات معينة بسرعة
             مفيد جدًا في LINQ
            */

            #endregion


            #region 4- Element Operators

            //var firstProduct = Products.First();
            //Console.WriteLine($"\nFirst Product: {firstProduct}");

            //var lastProduct = Products.Last();
            //Console.WriteLine($"Last Product: {lastProduct}");

            //var firstOutOfStock = Products
            //    .FirstOrDefault(p => p.UnitsInStock == 0);

            //Console.WriteLine(firstOutOfStock?.ProductName ?? "No Product Found");

            //try
            //{
            //    var singleProduct = Products.Single();
            //    Console.WriteLine($"Single Product: {singleProduct}");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error: {ex.Message}");
            //}

            /*
             الشرح:
             First() = أول عنصر (بيرمي Error لو فاضي)
             Last() = آخر عنصر

             FirstOrDefault() = آمن (بيرجع null لو مفيش)

             Single() = لازم عنصر واحد بس
             لو فيه أكتر من عنصر → Error

             علشان كده استخدمنا try-catch
            */

            #endregion

            #region 5- Aggregate Operators (Immediate Execution)

            // عدد المنتجات
            var numOfProducts = Products.Count();
            Console.WriteLine(numOfProducts);

            // عدد المنتجات اللي المخزون بتاعها = 0
            var numOfOutOfStock = Products.Count(p => p.UnitsInStock == 0);
            Console.WriteLine(numOfOutOfStock);

            // أكبر عنصر (لازم Product يكون فيه IComparable وإلا هيرمي Error)
            try
            {
                var maxProduct = Products.Max();
                Console.WriteLine(maxProduct);
            }
            catch
            {
                Console.WriteLine("Max() على Product محتاج Comparer");
            }

            // أعلى سعر
            var maxPrice = Products.Max(p => p.UnitPrice);
            Console.WriteLine(maxPrice);

            // أقل طول لاسم المنتج
            var minNameLength = Products.Min(p => p.ProductName.Length);
            Console.WriteLine(minNameLength);

            // المنتج اللي اسمه أقصر اسم
            var shortestNameProduct = Products
                .FirstOrDefault(p => p.ProductName.Length == minNameLength);

            Console.WriteLine(shortestNameProduct);

            // مجموع الأسعار
            var totalPrice = Products.Sum(p => p.UnitPrice);
            Console.WriteLine(totalPrice);

            // متوسط الأسعار
            var avgPrice = Products.Average(p => p.UnitPrice);
            Console.WriteLine(avgPrice);

            // Aggregate (تجميع مخصص)
            string[] names = { "Saif", "Aldin", "Ahmed", "Abdelbar" };

            var combinedNames = names.Aggregate((a, b) => $"{a} {b}");
            Console.WriteLine(combinedNames);

            /*
             الشرح:

             Aggregate Operators = عمليات حسابية على البيانات
             ودي Immediate Execution يعني بتتنفذ فورًا مش بتتأجل

             Count() = عدد العناصر
             Count(condition) = عدد عناصر بشرط

             Max() = أكبر قيمة
             ⚠️ لو على Object لازم يكون فيه مقارنة (IComparable)

             Max(selector) = أكبر قيمة لخاصية (زي السعر)

             Min() = أصغر قيمة
             استخدمناها هنا عشان نجيب أقصر اسم

             Sum() = مجموع القيم
             Average() = المتوسط الحسابي

             Aggregate() = أقوى واحدة
             بتعمل تجميع مخصص (زي دمج Strings أو حسابات معقدة)

             مثال:
             "Saif Aldin Ahmed Abdelbar"

             💡 الفرق المهم:
             Where / Select → Deferred Execution
             Count / Sum / Average → Immediate Execution

            */

            #endregion

            #region 6- Casting Operators (Immediate Execution)

            // تحويل لـ List
            List<Product> productList = Products
                .Where(p => p.UnitsInStock > 0)
                .ToList();

            // تحويل لـ Array
            Product[] productArray = Products
                .Where(p => p.UnitsInStock > 0)
                .ToArray();

            // تحويل لـ Dictionary (Key = ProductID)
            Dictionary<long, Product> productDictionary = Products
                .Where(p => p.UnitsInStock > 0)
                .ToDictionary(p => p.ProductID);

            // تحويل لـ HashSet (بيمنع التكرار)
            HashSet<Product> productSet = Products
                .Where(p => p.UnitsInStock > 0)
                .ToHashSet();

            /*
             الشرح:

             Casting Operators = تحويل نتيجة LINQ لنوع بيانات معين

             ToList()   → يحول النتيجة لـ List
             ToArray()  → يحولها لـ Array
             ToDictionary() → يحولها لـ Dictionary (لازم تحدد Key)
             ToHashSet() → يحولها لـ Set (بيشيل التكرار)

             💡 كلهم Immediate Execution
             يعني الكويري بيتنفذ فورًا مش بيتأجل

             ⚠️ مهم جدًا:

             ToDictionary():
             - لازم الـ Key يكون Unique
             - لو فيه تكرار → البرنامج هيكسر (Exception)

             مثال:
             لو ProductID مكرر → Error

             ToHashSet():
             - بيشيل العناصر المكررة
             - بيعتمد على Equals / GetHashCode

             💡 إمتى تستخدم كل واحد؟

             List      → الأكثر استخدامًا
             Array     → لو محتاج performance أعلى أو fixed size
             Dictionary→ لو هتبحث بسرعة بـ Key
             HashSet   → لو عايز تمنع التكرار

            */

            #endregion

            #region 7- Generation Operators (Immediate Execution)

            // Range: إنشاء أرقام من 1 إلى 100
            var range = Enumerable.Range(1, 100); // من 1 لـ 100 (مش 99)

            // Repeat: تكرار قيمة معينة
            var repeatedNumbers = Enumerable.Repeat(10, 3); // 10, 10, 10
            var repeatedStrings = Enumerable.Repeat("Hello", 5); // "Hello" x 5

            Console.WriteLine("Range:");
            foreach (var item in range)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nRepeated Numbers:");
            foreach (var item in repeatedNumbers)
            {
                Console.WriteLine(item);
            }

            // Empty: إنشاء Collection فاضية
            var emptyProducts = Enumerable.Empty<Product>().ToList();

            /*
             الشرح:

             Generation Operators = إنشاء بيانات جديدة من غير مصدر خارجي

             Range(start, count):
             - بيعمل Sequence من الأرقام
             - يبدأ من start ويطلع count رقم
             مثال: Range(1,100) → من 1 لـ 100

             Repeat(value, count):
             - بيكرر قيمة معينة عدد مرات
             مثال: Repeat(10,3) → 10,10,10

             Empty<T>():
             - بيرجع Collection فاضية من نوع معين
             مفيد بدل ما تعمل new List<T>()

             💡 كلهم Immediate Execution (لما تستخدمهم مع Loop أو ToList)

             ⚠️ ملاحظات:
             - Range بيشتغل مع int بس
             - Repeat ممكن يستخدم لأي نوع (int / string / object)

             💡 استخدامات حقيقية:
             - Testing
             - Dummy Data
             - Initialization سريع

            */

            #endregion

            #region 8- Set Operators

            var seq01 = Enumerable.Range(0, 100);   // 0 → 99
            var seq02 = Enumerable.Range(50, 100);  // 50 → 149

            // Union: دمج بدون تكرار
            var union = seq01.Union(seq02);

            // Concat: دمج مع التكرار
            var concat = seq01.Concat(seq02);

            // Distinct: إزالة التكرار
            var distinct = concat.Distinct();

            // Intersect: العناصر المشتركة
            var intersect = seq01.Intersect(seq02);

            // Except: العناصر اللي في seq01 ومش في seq02
            var except = seq01.Except(seq02);


            // عرض النتائج (مثال سريع)
            Console.WriteLine("Union:");
            foreach (var item in union)
                Console.Write(item + " ");

            Console.WriteLine("\n\nIntersect:");
            foreach (var item in intersect)
                Console.Write(item + " ");

            Console.WriteLine("\n\nExcept:");
            foreach (var item in except)
                Console.Write(item + " ");


            /*
             الشرح:

             Set Operators = عمليات على مجموعات (زي الرياضيات)

             Union():
             - بيجمع الاتنين بدون تكرار
             النتيجة: 0 → 149

             Concat():
             - بيجمع الاتنين مع التكرار
             الأرقام من 50 → 99 هتتكرر

             Distinct():
             - بيشيل التكرار
             زي Union لكن بيشتغل على Sequence واحدة

             Intersect():
             - بيرجع العناصر المشتركة
             النتيجة: 50 → 99

             Except():
             - بيرجع العناصر اللي في الأولى بس
             النتيجة: 0 → 49

             💡 ملاحظات مهمة:

             - كل العمليات دي Deferred Execution
             يعني مش بتتنفذ غير مع foreach أو ToList()

             - المقارنة بتتم باستخدام:
               Equals() و GetHashCode()

             - لو شغال على Objects (زي Product)
             لازم تعمل override للـ Equals و GetHashCode
             عشان النتائج تبقى صح

             💡 استخدامات حقيقية:
             - مقارنة بيانات
             - إزالة التكرار
             - معرفة الفرق بين Lists
             - دمج مصادر بيانات

            */

            #endregion

            #region 9- Quantifiers Operators (Return Boolean Value)

            // هل فيه أي عنصر في الـ Collection؟
            Console.WriteLine(Products.Any());

            // هل فيه منتج واحد على الأقل المخزون بتاعه = 0؟
            Console.WriteLine(Products.Any(p => p.UnitsInStock == 0));

            // هل كل المنتجات ProductID بتاعها = 1 ؟
            Console.WriteLine(Products.All(p => p.ProductID == 1));


            /*
             الشرح:

             Quantifiers Operators = بيرجعوا true أو false

             Any():
             - بيرجع true لو فيه عنصر واحد على الأقل
             - لو Collection فاضية → false

             Any(condition):
             - بيرجع true لو فيه عنصر يحقق الشرط

             مثال:
             Products.Any(p => p.UnitsInStock == 0)
             → هل فيه منتج ناقص في المخزون؟

             --------------------------------------

             All():
             - بيرجع true لو كل العناصر تحقق الشرط
             - لو فيه عنصر واحد بس مخالف → false

             مثال:
             Products.All(p => p.ProductID == 1)
             → هل كل المنتجات ID بتاعها = 1 ؟ (غالبًا false)

             --------------------------------------

             💡 الفرق المهم:

             Any()  → بيدور على "واحد على الأقل"
             All()  → بيتأكد إن "الكل"

             --------------------------------------

             💡 استخدامات حقيقية:

             - Validation
             - Checking data existence
             - Conditions بدل loops

             مثال عملي:
             if (!Products.Any())
             → مفيش بيانات

             if (Products.Any(p => p.UnitsInStock == 0))
             → فيه منتجات ناقصة

            */

            #endregion

            #region 10- Zip Operator

            string[] namess = { "Saif", "Aldin", "Ahmed", "Abdelbar" };
            var numbers = Enumerable.Range(1, 10);

            // دمج Array الأسماء مع الأرقام
            var zipped = namess.Zip(numbers, (name, num) => new
            {
                Name = name,
                Number = num
            });

            // عرض النتيجة
            foreach (var item in zipped)
            {
                Console.WriteLine($"{item.Name} - {item.Number}");
            }


            /*
             الشرح:

             Zip() = دمج عنصر من List مع عنصر من List تانية

             بيشتغل كده:
             (الأول من الأولى + الأول من التانية)
             (التاني من الأولى + التاني من التانية)
             وهكذا...

             --------------------------------------

             مثال:
             names = { A, B, C }
             nums  = { 1, 2, 3 }

             النتيجة:
             A-1
             B-2
             C-3

             --------------------------------------

             ⚠️ ملاحظة مهمة:

             Zip بيقف عند أقصر List

             مثال:
             names = 4 عناصر
             nums = 10 عناصر

             النتيجة = 4 عناصر بس

             --------------------------------------

             💡 استخداماته:

             - ربط بيانات ببعض (اسم + رقم)
             - دمج Lists
             - مقارنة عنصرين

             --------------------------------------

             💡 ملاحظة:

             Zip() غالبًا استخدامه قليل في الشغل
             لكنه مفيد في حالات معينة

            */

            #endregion

            #region 11- Grouping Operators

            // Query Syntax
            var groupingByCategory = from p in Products
                                     where p.UnitsInStock > 0
                                     group p by p.Category;

            foreach (var group in groupingByCategory)
            {
                Console.WriteLine($"Category: {group.Key}");

                foreach (var product in group)
                {
                    Console.WriteLine($"  {product.ProductName}");
                }
            }


            // Method Syntax
            var groupById = Products.GroupBy(p => p.ProductID);

            foreach (var group in groupById)
            {
                Console.WriteLine($"ProductID: {group.Key}");

                foreach (var product in group)
                {
                    Console.WriteLine($"  {product.ProductName}");
                }
            }


            // Grouping مع حسابات (مهم جدًا)
            var groupWithStats = Products
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Count = g.Count(),
                    MaxPrice = g.Max(p => p.UnitPrice),
                    AvgPrice = g.Average(p => p.UnitPrice)
                });

            foreach (var item in groupWithStats)
            {
                Console.WriteLine($"Category: {item.Category}");
                Console.WriteLine($"Count: {item.Count}");
                Console.WriteLine($"Max Price: {item.MaxPrice}");
                Console.WriteLine($"Average Price: {item.AvgPrice}");
                Console.WriteLine("----------------------");
            }


            /*
             الشرح:

             GroupBy = تقسيم البيانات لمجموعات

             كل Group بيكون عبارة عن:
             - Key → القيمة اللي عملت grouping عليها
             - مجموعة عناصر ليها نفس الـ Key

             --------------------------------------

             مثال:
             GroupBy(p => p.Category)

             النتيجة:
             Category1 → [Products...]
             Category2 → [Products...]

             --------------------------------------

             Query Syntax:
             group p by p.Category

             Method Syntax:
             Products.GroupBy(p => p.Category)

             --------------------------------------

             أهم حاجة تفهمها:

             group.Key → اسم المجموعة
             group → العناصر جوه المجموعة

             --------------------------------------

             أقوى استخدام (في الشغل):

             Group + Aggregate
             زي:
             - Count()
             - Max()
             - Average()

             --------------------------------------

             مثال عملي:
             لكل Category:
             - عدد المنتجات
             - أعلى سعر
             - متوسط السعر

             --------------------------------------

             💡 ملاحظات:

             - GroupBy من أهم الحاجات في LINQ
             - بيستخدم في التقارير (Reports)
             - أساس أي Dashboard

            */

            #endregion

            #region 12- Partitioning Operators

            // Take: أول 10 عناصر
            var take = Products
                .Where(p => p.UnitPrice > 0)
                .Take(10)
                .ToList(); // 🔥 مهم (Immediate Execution)

            Console.WriteLine("Take (First 10):");
            foreach (var item in take)
            {
                Console.WriteLine($"{item.ProductName} - {item.UnitPrice}");
            }

            // أعلى سعر من اللي اتاخدوا
            var maxPricea = take.Max(p => p.UnitPrice);
            Console.WriteLine($"Max Price (From Taken): {maxPricea}");


            // TakeLast: آخر 10 عناصر
            var takeLast = Products
                .Where(p => p.UnitPrice > 0)
                .TakeLast(10)
                .ToList();

            Console.WriteLine("\nTakeLast (Last 10):");
            foreach (var item in takeLast)
            {
                Console.WriteLine($"{item.ProductName} - {item.UnitPrice}");
            }


            // Skip: تجاهل أول 10 عناصر
            var skip = Products
                .Where(p => p.UnitPrice > 0)
                .Skip(10)
                .ToList();

            Console.WriteLine("\nSkip (After First 10):");
            foreach (var item in skip)
            {
                Console.WriteLine($"{item.ProductName} - {item.UnitPrice}");
            }


            // SkipLast: تجاهل آخر 10 عناصر
            var skipLast = Products
                .Where(p => p.UnitPrice > 0)
                .SkipLast(10)
                .ToList();

            Console.WriteLine("\nSkipLast (Remove Last 10):");
            foreach (var item in skipLast)
            {
                Console.WriteLine($"{item.ProductName} - {item.UnitPrice}");
            }


            // TakeWhile: خد لحد ما الشرط يوقف
            var takeWhile = Products
                .Where(p => p.UnitPrice > 0)
                .TakeWhile(p => p.UnitPrice < 50)
                .ToList();

            Console.WriteLine("\nTakeWhile (Price < 50):");
            foreach (var item in takeWhile)
            {
                Console.WriteLine($"{item.ProductName} - {item.UnitPrice}");
            }


            // SkipWhile: تجاهل لحد ما الشرط يوقف
            var skipWhile = Products
                .Where(p => p.UnitPrice > 0)
                .SkipWhile(p => p.UnitPrice < 50)
                .ToList();

            Console.WriteLine("\nSkipWhile (Skip Price < 50):");
            foreach (var item in skipWhile)
            {
                Console.WriteLine($"{item.ProductName} - {item.UnitPrice}");
            }


            /*
             الشرح:

             Partitioning Operators = تقسيم البيانات (أخد جزء منها)

             --------------------------------------

             Take(n):
             - بياخد أول n عنصر

             TakeLast(n):
             - بياخد آخر n عنصر

             Skip(n):
             - بيتجاهل أول n عنصر

             SkipLast(n):
             - بيتجاهل آخر n عنصر

             --------------------------------------

             TakeWhile(condition):
             - بياخد العناصر طالما الشرط True
             - أول ما الشرط يفشل → يقف

             SkipWhile(condition):
             - يتجاهل العناصر طالما الشرط True
             - أول ما الشرط يفشل → يبدأ ياخد

             --------------------------------------

             ⚠️ ملاحظة مهمة جدًا:

             TakeWhile / SkipWhile بيعتمدوا على ترتيب البيانات

             يعني:
             لو البيانات مش مرتبة → ممكن يدي نتائج غريبة

             --------------------------------------

             💡 استخدامات حقيقية:

             - Pagination (صفحات)
               Skip + Take

             مثال:
             page 2:
             Skip(10).Take(10)

             --------------------------------------

             💡 ملاحظة:

             ToList() هنا مهم لأنه:
             - بيحول لـ Immediate Execution
             - بيثبت النتيجة

            */

            #endregion

            #region 13- Let and Into

            using System.Text.RegularExpressions;

            // List الأسماء
            List<string> names = new List<string> { "Ahmed", "Saif", "Haneen", "Leen", "Mai" };

            // Replace الحروف المتحركة بـ *
            var replaced = from n in names
                           select Regex.Replace(n, "[aeiouAEIOU]", "*");

            Console.WriteLine("Replaced Names:");
            foreach (var item in replaced)
            {
                Console.WriteLine(item);
            }


            // استخدام let (متغير مؤقت)
            var withLength = from n in names
                             let length = n.Length
                             select new
                             {
                                 Name = n,
                                 Length = length
                             };

            Console.WriteLine("\nNames with Length:");
            foreach (var item in withLength)
            {
                Console.WriteLine($"{item.Name} - {item.Length}");
            }


            // استخدام into (تكملة Query بعد Grouping)
            var grouped = from n in names
                          group n by n.Length into g
                          select new
                          {
                              Length = g.Key,
                              Count = g.Count(),
                              Names = g
                          };

            Console.WriteLine("\nGrouped by Length:");
            foreach (var group in grouped)
            {
                Console.WriteLine($"Length: {group.Length} - Count: {group.Count}");

                foreach (var name in group.Names)
                {
                    Console.WriteLine($"  {name}");
                }
            }


            /*
             الشرح:

             let:
             - بيستخدم لإنشاء متغير مؤقت داخل الـ Query
             - بدل ما تحسب نفس الحاجة أكتر من مرة

             مثال:
             let length = n.Length

             --------------------------------------

             into:
             - بيستخدم علشان تكمل Query بعد group أو select
             - بيديك اسم جديد للنتيجة

             مثال:
             group n by n.Length into g

             --------------------------------------

             💡 الفرق:

             let   → متغير مؤقت
             into  → استمرار للـ Query

             --------------------------------------

             💡 استخدامات:

             let:
             - تحسين الأداء
             - تنظيم الكود

             into:
             - مع GroupBy
             - بناء Queries معقدة

             --------------------------------------

             ⚠️ ملاحظات:

             - let و into بيستخدموا مع Query Syntax بس
             - Method Syntax مفيهاش let و into بنفس الشكل

            */

            #endregion

        }
    }
}