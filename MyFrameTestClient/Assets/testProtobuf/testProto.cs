//using UnityEngine;
//using Google.Protobuf;
//namespace MainClient {
//    public class testProto : MonoBehaviour
//    {

//        void Start()
//        {
//            ComplexObject test = new ComplexObject();

//            test.Id=200;//普通变量赋值

//            //list变量赋值
//            //先初始化类型
//            Result res = new Result();
//            res.Title = "a";
//            //放入repeated
//            test.Sons.Add(res);

//            Result res2 = new Result();
//            res2.Title = "b";
//            test.Sons.Add(res2);
//            foreach(var i in test.Sons)
//            {
//                Debug.Log(i.Title);
//            }
//            ///map [string , list] 使用方法
//            ///这个list 在proto中必须是个message单独的，不能和map 同级，具体看proto
//            ///先初始化list
//            list testList = new list();
//            Result newResult = new Result();
//            newResult.Title = "c";
//            testList.Ele.Add(newResult);

             
//            Result newResult2 = new Result();
//            newResult2.Title = "d";
//            testList.Ele.Add(newResult2);

//            list testList2 = new list();
//            Result newResult3 = new Result();
//            newResult3.Title = "e";
//            testList2.Ele.Add(newResult2);

//            Result newResult4 = new Result();
//            newResult4.Title = "f";
//            testList2.Ele.Add(newResult2);

//            //放入map
//            test.MapList["c"] = testList;
//            test.MapList["d"] = testList2;

//            //得到对应的map[key] 的value
//            list ans = new list();
//            Debug.Log(test.MapList.TryGetValue("c",out ans));
//            foreach (var i in ans.Ele)
//            {
//                Debug.Log("map key = c:  value="+ i.Title);
//            }
//        }

//    }
//}