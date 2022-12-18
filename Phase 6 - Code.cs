using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


namespace URS_2
{
    [Serializable]
    class COURSE
    {
        int ID;
        int Time;
        int Credit_Hours;
        int Capacity;
        int Number_of_Reg;
        int Instructor_ID;
        int[] Student_ID;
        public int getID
        {
            get
            {
                return ID;
            }
        }
        public int getTime
        {
            get
            {
                return Time;
            }
        }
        public int getCredit
        {
            get
            {
                return Credit_Hours;
            }
        }
        public int getINSTRUCTOR
        {
            get
            {
                return Instructor_ID;
            }
        }
        public int getNumberOfReg
        {
            get
            {
                return Number_of_Reg;
            }
        }
        public COURSE(int c_id, int c_time, int c_credit_hours, int c_capacity, int c_instr)
        {
            ID = c_id;
            Time = c_time;
            Credit_Hours = c_credit_hours;
            Capacity = c_capacity;
            Number_of_Reg = 0;
            Instructor_ID = c_instr;
            Student_ID = new int[100];
        }
        public void Course_print() //done
        {
            //get instructor file and using Instructor_ID to show instructor info
            int c1 = 0;
            int c = 0;
            FileStream instructor_file = new FileStream("INSTRUCTOR.txt", FileMode.Open, FileAccess.Read);
            INSTRUCTOR[] instructor_list = new INSTRUCTOR[100];
            try
            {
                BinaryFormatter instructor_binary = new BinaryFormatter();

                while (instructor_file.Position < instructor_file.Length)
                {
                    instructor_list[c1] = (INSTRUCTOR)instructor_binary.Deserialize(instructor_file);
                    c1++;
                }
            }
            catch (SerializationException e)
            {
                Console.WriteLine(" Failed to deSerialize instructor. Reason: " + e.Message);
                throw;
            }
            finally
            {
                instructor_file.Close();
            }
            
            
            while (c < c1)
            {
                if ((instructor_list[c].getID) == Instructor_ID)
                {
                    break;
                }
                c++;
            }
            if(c!=c1)
               Console.WriteLine("\n " + ID + "\t" + Time + "\t" + Credit_Hours + "\t\t" + Capacity + "\t\t" + Number_of_Reg + "\t\t" + instructor_list[c].getUSER + "\t\t" + Instructor_ID);

        }//////////////////////////////////////////////////////////////////

        public void reg_student_print()//done
        {
            //get student file and using students_id[] to show students info
            FileStream student_file = new FileStream("STUDENT.txt", FileMode.Open, FileAccess.Read);
            STUDENT[] student_list = new STUDENT[100];
            int i = 0;
            try
            {
                BinaryFormatter student_binary = new BinaryFormatter();


                while (student_file.Position < student_file.Length)
                {
                    student_list[i] = (STUDENT)student_binary.Deserialize(student_file);
                    i++;
                }
            }
            catch (SerializationException e)
            {
                Console.WriteLine(" Failed to deSerialize student. Reason: " + e.Message);
                throw;
            }
            finally
            {
                student_file.Close();
            }


            Console.WriteLine("\n registered student :\n");
            Console.WriteLine(" ID\tNAME\tMAJORS\n");
            for (int c = 0; c < i; c++)
            {
                for (int j = 0; j <= Number_of_Reg; j++)
                {

                    if (Student_ID[j] == student_list[c].getID)
                    {
                        Console.WriteLine(" " + student_list[c].getID + "\t" + student_list[c].getUser + "\t" + student_list[c].getMager);
                        j = Number_of_Reg;
                    }
                }
            }

        }
        public void AddStudent(int s)//done
        {
            Student_ID[Number_of_Reg] = s;
            Number_of_Reg++;
        }
        public void DeleatStudent(int s)//done
        {
            var temp_Student = Student_ID.ToList();
            for (int k = 0; k <= Number_of_Reg; k++)
            {
                if (temp_Student[k] == s)
                {

                    temp_Student.RemoveAt(k);

                    Number_of_Reg -= 1;



                }
            }
            Student_ID = temp_Student.ToArray();



        }
        public bool isfull()
        {
            if (Capacity == Number_of_Reg)
                return true;
            return false;
        }

    }


    [Serializable]
    class STUDENT
    {
        int ID;
        string User;
        string Mager;
        int[] MyCourse_ID;
        int[] busy_time;
        int MyCourse_Num;
        int MyCourse_Credit;
        public STUDENT( string u, int i, string m)
        {
            MyCourse_ID = new int[100];
            busy_time = new int[100];
            ID = i;
            User = u;
            Mager = m;
            MyCourse_Num = 0;
            MyCourse_Credit = 0;
        }
        public void Student_Print()
        {
            Console.WriteLine(ID + " " + User + " " + Mager);
        }
        public int getID
        {
            get
            {
                return ID;
            }
        }
        public string getMager
        {
            get
            {
                return Mager;
            }
        }
        public string getUser
        {
            get
            {
                return User;
            }
        }

        public void Deleat_Course(int c_id,int c_credit)//done
        {
            var temp_mycours = MyCourse_ID.ToList();
            for (int y = 0; y < MyCourse_Num; y++)
            {
                if (MyCourse_ID[y] == c_id)
                {
                    temp_mycours.RemoveAt(y);
                    MyCourse_Num--;
                    MyCourse_Credit -= c_credit;
                }
            }
            MyCourse_ID = temp_mycours.ToArray();
        }
        public bool not_busy(int t)
        {
            for (int i = 0; i < MyCourse_Num; i++)
            {
                if (busy_time[i] == t)
                    return false;
            }

            return true;
        }
        public void Drop_a_course()//done
        {
            int cc = 0,j;
            int c_id=0;
            Console.WriteLine("\n ID\tTime\tCredit Hours\tCapacity\tNumber of Reg\tInstructor\t instructor ID");
            FileStream course_file = new FileStream("COURSE.txt", FileMode.Open, FileAccess.Read);
            COURSE[] course_list = new COURSE[100];
            try
            {
                BinaryFormatter course_binary = new BinaryFormatter();
                while (course_file.Position < course_file.Length)
                {
                    course_list[cc] = (COURSE)course_binary.Deserialize(course_file);
                    cc++;
                }
            }
            catch (SerializationException e)
            {
                Console.WriteLine(" Failed to deSerialize course. Reason: " + e.Message);
                throw;
            }
            finally
            {
                course_file.Close();
            }
            for(int i=0;i<MyCourse_Num;i++)
            {
                for(int m=0;m<cc;m++)
                {
                    if (MyCourse_ID[i] == course_list[m].getID)
                    {
                        course_list[m].Course_print();
                    }
                }
               
            }
            Console.Write("Enter course ID:");
            try
            {
                c_id = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.Write("invalid input");
            }
            j = 0;
            var temp_mycours = MyCourse_ID.ToList();
            var temp_time = busy_time.ToList();
            while (j < cc)
            {
                if (course_list[j].getID == c_id)
                {
                    for (int y = 0; y < MyCourse_Num; y++)
                    {
                        if (MyCourse_ID[y] == course_list[j].getID)
                        {
                            temp_mycours.RemoveAt(y);
                            temp_time.RemoveAt(y);
                            course_list[j].DeleatStudent(ID);
                            MyCourse_Num--;
                            MyCourse_Credit -= course_list[j].getCredit;
                        }
                    }

                }
                j++;
                if (j == cc)
                {
                    Console.WriteLine("this course not found");
                    Console.ReadKey();
                }
            }
            MyCourse_ID = temp_mycours.ToArray();
            busy_time = temp_time.ToArray();

            FileStream course_file_1 = new FileStream("COURSE.txt", FileMode.Create, FileAccess.Write);
           
            try
            {
                BinaryFormatter course_binary_1 = new BinaryFormatter();
                for(int i=0;i<cc;i++)
                {
                    course_binary_1.Serialize(course_file_1, course_list[i]);
                    
                }
            }
            catch (SerializationException e)
            {
                Console.WriteLine(" Failed to Serialize course. Reason: " + e.Message);
                throw;
            }
            finally
            {
                course_file_1.Close();
            }

        }
        public void Register_for_a_course()//done
        {
            int cc = 0, j;
            int c_id=-1;
            FileStream course_file = new FileStream("COURSE.txt", FileMode.Open, FileAccess.Read);
            COURSE[] course_list = new COURSE[100];
            Console.WriteLine("\n ID\tTime\tCredit Hours\tCapacity\tNumber of Reg\tInstructor\t instructor ID");
            try
            {
                BinaryFormatter course_binary = new BinaryFormatter();
                while (course_file.Position < course_file.Length)
                {
                    course_list[cc] = (COURSE)course_binary.Deserialize(course_file);
                    course_list[cc].Course_print();
                    cc++;
                }
            }
            catch (SerializationException e)
            {
                Console.WriteLine(" Failed to deSerialize course. Reason: " + e.Message);
                throw;
            }
            finally
            {
                course_file.Close();
            }

            Console.Write("Enter course ID:");
            try
            {
                c_id = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.Write("invalid input");
            }
            
            j = 0;
            bool flag;
            while (j < cc)
            {
                flag = false;
                if (course_list[j].getID == c_id)
                {
                    for (int y = 0; y < MyCourse_Num; y++)
                    {
                        if (MyCourse_ID[y] == course_list[j].getID)
                        {
                            flag = true;
                        }
                        
                    }
                    if (!not_busy(course_list[j].getTime))
                    {
                        flag = true;
                    }
                    if (course_list[j].isfull())
                    {
                        flag = true;
                    }

                    if (!flag)
                    {
                        MyCourse_ID[MyCourse_Num] = course_list[j].getID;
                        course_list[j].AddStudent(ID);

                        MyCourse_Credit += course_list[j].getCredit;
                        MyCourse_Num++;
                    }
                    else
                    {
                        Console.WriteLine("You are currently enrolled in this course");

                    }
                    j = cc;
                }
                j++;
            }
            if(j==cc)
            {
                Console.WriteLine("this course not found");
                Console.ReadKey();
            }
            FileStream course_file_1 = new FileStream("COURSE.txt", FileMode.Create, FileAccess.Write);
            try
            {
                BinaryFormatter course_binary_1 = new BinaryFormatter();
                for (int k = 0; k < cc; k++)
                {

                    course_binary_1.Serialize(course_file_1, course_list[k]);
                }
            }
            catch (SerializationException e)
            {
                Console.WriteLine(" Failed to Serialize course. Reason: " + e.Message);
                throw;
            }
            finally
            {
                course_file_1.Close();
            }

        }
        public void Show_the_completed_student_schedule()
        {
            int cc = 0, j;
            
            Console.WriteLine("\n ID\tTime\tCredit Hours\tCapacity\tNumber of Reg\tInstructor\t instructor ID");
            FileStream course_file = new FileStream("COURSE.txt", FileMode.Open, FileAccess.Read);
            COURSE[] course_list = new COURSE[100];
            try
            {
                BinaryFormatter course_binary = new BinaryFormatter();
                while (course_file.Position < course_file.Length)
                {
                    course_list[cc] = (COURSE)course_binary.Deserialize(course_file);
                    
                    cc++;
                }
            }
            catch (SerializationException e)
            {
                Console.WriteLine(" Failed to deSerialize course. Reason: " + e.Message);
                throw;
            }
            finally
            {
                course_file.Close();
            }
            j = 0;

            while (j < cc)
            {
                for (int k = 0; k < MyCourse_Num; k++)
                {
                    if (MyCourse_ID[k] == course_list[j].getID)
                    {
                        course_list[j].Course_print();
                        k = MyCourse_Num;
                    }
                }
                j++;
            }
            Console.WriteLine("\n Total credit hours is :  " + MyCourse_Credit+"\n");
        }
    }


    
    [Serializable]
    class INSTRUCTOR
    {
        string User;
        int ID;
        int[] MyCourse_ID;
        int[] busy_time;
        int MyCourse_NUM;
        
        public INSTRUCTOR(string u, int i)
        {
            ID = i;
            User = u;
            MyCourse_ID = new int[100];
            busy_time = new int[100];
            MyCourse_NUM = 0;
        }
        public void Instructor_print()
        {
            //not need load course file and using mycours_id to show all courses info ____ not need ==> using cou_print() reg_student_print()
            Console.WriteLine("\n" +User + "  " + ID + "\n");
        }
        public bool not_busy(int t)
        {
            for(int i=0;i<MyCourse_NUM;i++)
            {
                if (busy_time[i] == t)
                    return false;
            }

            return true;
        }
     
        public int getID
        {
            get
            {
                return ID;
            }
        }
        public string getUSER
        {
            get
            {
                return User;
            }
        }

        public void AddCourse(int s,int t)
        {
            MyCourse_ID[MyCourse_NUM] = s;
            busy_time[MyCourse_NUM] = t;
            MyCourse_NUM++;
        }
        public void DeleatCours(int s)
        {
            var mycours_list = MyCourse_ID.ToList();
            for (int i = 0; i < MyCourse_NUM; i++)
            {
                if (MyCourse_ID[i] == s)
                {
                    mycours_list.RemoveAt(i);
                    MyCourse_NUM--;
                }
            }
            MyCourse_ID = mycours_list.ToArray();
        }

        public void Show_a_student_roster_for_a_given_course()//done
        {

            //load course file and using mycours_id to show all courses info using cou_print() reg_student_print()
            int cc = 0, j;
            int c_id;
            Console.WriteLine("\n ID\tTime\tCredit Hours\tCapacity\tNumber of Reg\tInstructor\t instructor ID");
            FileStream course_file = new FileStream("COURSE.txt", FileMode.Open, FileAccess.Read);
            COURSE[] course_list = new COURSE[100];
            try
            {
                BinaryFormatter course_binary = new BinaryFormatter();
                while (course_file.Position < course_file.Length)
                {
                    
                    course_list[cc] = (COURSE)course_binary.Deserialize(course_file);                  
                    
                    cc++;
                }
            }
            catch (SerializationException e)
            {
                Console.WriteLine(" Failed to deSerialize course. Reason: " + e.Message);
                throw;
            }
            finally
            {
                course_file.Close();
            }

            j = 0;
            while (j < cc)
            {
                for (int k = 0; k < MyCourse_NUM; k++)
                {
                    if (MyCourse_ID[k] == course_list[j].getID)
                    {
                        course_list[j].Course_print();
                        k = MyCourse_NUM;
                    }
                }
                j++;
            }
            Console.Write("Enter course ID:");
            try
            {
                c_id = Convert.ToInt32(Console.ReadLine());
                j = 0;

                while (j < cc)
                {
                    for (int k = 0; k < MyCourse_NUM; k++)
                    {
                        if (MyCourse_ID[k] == course_list[j].getID)
                        {
                            course_list[j].Course_print();
                            course_list[j].reg_student_print();
                            k = MyCourse_NUM;
                        }
                    }
                    j++;
                }
            }
           catch
            {
                Console.Write("invalid input");
                c_id = -1;
            }

            

            
        }
    }


    
    [Serializable]
    class REGISTRAR
    {
        string User;
        int ID;
        int[] Courses;
        int Courses_Num = 0;
        public REGISTRAR(string u, int i)
        {
            ID = i;
            User = u;
            Courses = new int[100];
            Courses_Num = 0;
        }
        public string getUser
        {
            get
            {
                return User;
            }
        }
        public int getID
        {
            get
            {
                return ID;
            }
        }

        public void Add_new_course()
        {
            int c_id=-1, c_time=-1, c_credit_hours=-1, c_capacity=-1, c_in_id=-1;
            int i = 0; //counter
            int j = 0; //counter
            int c = 0; //counter
            Console.Write(" Enter course ID:");
            try
            {
                c_id = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("invalid input");
                j = -1;
            }

            if (c_id != -1)
            {
                Console.Write(" Enter course Time:");
                try
                {

                    c_time = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("invalid input");
                    j = -1;
                }
            }
            if(c_time!=-1)
            {
                Console.Write(" Enter course credit hours:");
                try
                {
                    c_credit_hours = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("invalid input");
                    j = -1;
                }
            }
            if (c_credit_hours != -1)
            {
                Console.Write(" Enter course capacity:");
                try
                {
                    c_capacity = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("invalid input");
                    j = -1;
                }
            }

            
            //---------------------------------open instructor file and display them--------------------------//
            FileStream instructor_file = new FileStream("INSTRUCTOR.txt", FileMode.Open, FileAccess.Read);
            INSTRUCTOR[] instructor_list = new INSTRUCTOR[100]; 
            try
            {
                BinaryFormatter instructor_binary = new BinaryFormatter();
                while (instructor_file.Position < instructor_file.Length)
                {
                    instructor_list[i] = (INSTRUCTOR)instructor_binary.Deserialize(instructor_file);
                    i++;
                }
            }
            catch (SerializationException e)
            {
                Console.WriteLine(" Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                instructor_file.Close();
            }
            
            
            //------------------------------------------------------------------------------------------------//
            if(c_capacity!=-1)
            {
                while (j < i)
                {
                    instructor_list[j].Instructor_print();
                    j++;
                }
                Console.WriteLine("\n Enter course instrutor ID:");
                try
                {
                    c_in_id = Convert.ToInt32(Console.ReadLine());
                    j = 0;
                    while (j < i)
                    {
                        if (instructor_list[j].getID == c_in_id)
                        {
                            break;
                        }
                        j++;
                    }
                }
                catch
                {
                    Console.WriteLine("invalid input");
                    j = -1;
                }
            }
            
           
            if(j==-1)
            {

            }
            else if(j==i)
            {
                Console.WriteLine("\n The instrutor not found the course not added");
            }
            else if(instructor_list[j].not_busy(c_time))
            {
                // add the new course to file COURSE.txt
                Courses[Courses_Num] = c_in_id;
                COURSE newcourse = new COURSE(c_id, c_time, c_credit_hours, c_capacity, c_in_id);
                instructor_list[j].AddCourse(c_id, c_time);

                FileStream course_file = new FileStream("COURSE.txt", FileMode.Append, FileAccess.Write);
                try
                {
                    BinaryFormatter course_binary = new BinaryFormatter();
                    course_binary.Serialize(course_file, newcourse);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine(" Failed to Serialize course. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    course_file.Close();
                }

                Courses_Num++;

                c = 0;
                FileStream instructor_file_1 = new FileStream("INSTRUCTOR.txt", FileMode.Open, FileAccess.Write);
                try
                {
                    BinaryFormatter instructor_binary_1 = new BinaryFormatter();
                    while (c < i)
                    {
                        instructor_binary_1.Serialize(instructor_file_1, instructor_list[c]);
                        c++;
                    }
                }
                catch (SerializationException e)
                {
                    Console.WriteLine(" Failed to Serialize instructor. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    instructor_file_1.Close();
                }

            }
            else
            {
                Console.WriteLine("\n The instrutor have a course at this time the course not added");
            }

        }
        //---------------------------------------------------------------------------------------------------//
        public void Cancel_exiting_course()
        {
            int c_id = -1, c, cc = 0, sc = 0, ic = 0;
            //-----------------------------------open course file -------------------------------------------//
            Console.WriteLine("\n ID\tTime\tCredit Hours\tCapacity\tNumber of Reg\tInstructor\t instructor ID");
            FileStream course_file = new FileStream("COURSE.txt", FileMode.Open, FileAccess.Read);
            COURSE[] course_list = new COURSE[100];
            try
            {
                BinaryFormatter course_binary = new BinaryFormatter();
                while (course_file.Position < course_file.Length)
                {
                    course_list[cc] = (COURSE)course_binary.Deserialize(course_file);
                    if (course_list[cc].getNumberOfReg < 3)
                    {
                        course_list[cc].Course_print();
                    }
                    cc++;
                }
            }
            catch (SerializationException e)
            {
                Console.WriteLine(" Failed to deSerialize course. Reason: " + e.Message);
                throw;
            }
            finally
            {
                course_file.Close();
            }

            Console.Write("Enter the course ID : ");
            try
            {
                c_id = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("invalid input");
                c_id = -1;
            }
            //------------------------------------------------------------------------------------------------//
            //---------------------------------open instructor file -------------------------------------------//
            FileStream instructor_file = new FileStream("INSTRUCTOR.txt", FileMode.Open, FileAccess.Read);
            INSTRUCTOR[] instructor_list = new INSTRUCTOR[100];
            try
            {
                BinaryFormatter instructor_binary = new BinaryFormatter();
                while (instructor_file.Position < instructor_file.Length)
                {
                    instructor_list[ic] = (INSTRUCTOR)instructor_binary.Deserialize(instructor_file);
                    ic++;
                }
            }
            catch (SerializationException e)
            {
                Console.WriteLine(" Failed to deserialize instrctor. Reason: " + e.Message);
                throw;
            }
            finally
            {
                instructor_file.Close();
            }
            //------------------------------------------------------------------------------------------------//
            //---------------------------------open student file -------------------------------------------//
            FileStream student_file = new FileStream("STUDENT.txt", FileMode.Open, FileAccess.Read);
            STUDENT[] student_list = new STUDENT[100];
            try
            {
                BinaryFormatter student_binary = new BinaryFormatter();
                while (student_file.Position < student_file.Length)
                {
                    student_list[sc] = (STUDENT)student_binary.Deserialize(student_file);
                    sc++;
                }
            }
            catch (SerializationException e)
            {
                Console.WriteLine(" Failed to deserialize student. Reason: " + e.Message);
                throw;
            }
            finally
            {
                student_file.Close();
            }
            //------------------------------------------------------------------------------------------------//
            //////////////////////////////find the course to cansel/////////////////////////////////////////////
            c = 0;
            while (c < cc)
            {
                if ((course_list[c].getID) == c_id)
                {
                    break;
                }
                c++;
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            if (c != cc)
            {


                for (int k = 0; k < sc; k++)
                {
                    student_list[k].Deleat_Course(course_list[c].getID, course_list[c].getCredit);
                }
                for (int k = 0; k < ic; k++)
                {
                    instructor_list[k].DeleatCours(course_list[c].getID);
                }

                //course_list
                ////////////////////////////////////////////////serialization////////////////////////////////////////////
                FileStream course_file_1 = new FileStream("COURSE.txt", FileMode.Create, FileAccess.Write);
                try
                {
                    BinaryFormatter course_binary_1 = new BinaryFormatter();
                    for (int k = 0; k < cc; k++)
                    {
                        if (course_list[k].getID != course_list[c].getID)
                        {
                            course_binary_1.Serialize(course_file_1, course_list[k]);
                        }
                    }
                }
                catch (SerializationException e)
                {
                    Console.WriteLine(" Failed to Serialize course. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    course_file_1.Close();
                }
                //---------------------------------------------------------------------------------------------------//
                FileStream instructor_file_1 = new FileStream("INSTRUCTOR.txt", FileMode.Create, FileAccess.Write);
                try
                {
                    BinaryFormatter instructor_binary_1 = new BinaryFormatter();
                    for (int k = 0; k < ic; k++)
                    {
                        instructor_binary_1.Serialize(instructor_file_1, instructor_list[k]);
                    }
                }
                catch (SerializationException e)
                {
                    Console.WriteLine(" Failed to serialize instrctor. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    instructor_file_1.Close();
                }
                //---------------------------------------------------------------------------------------------------//
                FileStream student_file_1 = new FileStream("STUDENT.txt", FileMode.Create, FileAccess.Write);

                try
                {
                    BinaryFormatter student_binary_1 = new BinaryFormatter();
                    for (int k = 0; k < sc; k++)
                    {
                        student_binary_1.Serialize(student_file_1, student_list[k]);
                    }
                }
                catch (SerializationException e)
                {
                    Console.WriteLine(" Failed to serialize student. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    student_file_1.Close();
                }
            }
        }
        //---------------------------------------------------------------------------------------------------//
        public void Show_all_active_courses()
        {
            int cc = 0;
            //-----------------------------------open course file -------------------------------------------//
            FileStream course_file = new FileStream("COURSE.txt", FileMode.Open, FileAccess.Read);
           
            COURSE[] course_list = new COURSE[100];
            try
            {
                BinaryFormatter course_binary = new BinaryFormatter();
                while (course_file.Position < course_file.Length)
                {
                    Console.WriteLine("\n ID\tTime\tCredit Hours\tCapacity\tNumber of Reg\tInstructor\t instructor ID");
                    course_list[cc] = (COURSE)course_binary.Deserialize(course_file);
                    course_list[cc].Course_print();
                    course_list[cc].reg_student_print();
                    Console.WriteLine("\n------------------------------------------------------------------------------------------\n");
                    cc++;
                }
            }
            catch (SerializationException e)
            {
                Console.WriteLine(" Failed to deSerialize course. Reason: " + e.Message);
                throw;
            }
            finally
            {
                course_file.Close();
            }
            Console.WriteLine();
        }

    }




    class Program
    {
        static int logintype()
        {
            int x=-1;
            Console.WriteLine("1.REGISTRAR");
            Console.WriteLine("2.STUDENT");
            Console.WriteLine("3.INSTRUCTOR");
            Console.WriteLine("4.EXIT");
            Console.Write("enter your choois:");
            try
            {
                x = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("invalid input");
                Console.ReadKey();
            }
            
            return x;
        }
        static int regfun()
        {
            int x=-1;
            Console.WriteLine("1.Add new course");
            Console.WriteLine("2.Cancel exiting course");
            Console.WriteLine("3.Show all active courses");
            Console.WriteLine("4.LogOut");
            Console.Write("enter your choois:");
            
            try
            {
                x = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("invalid input");
                Console.ReadKey();
            }
            return x;
        }
        static int stdfun()
        {
            int x=-1;
            Console.WriteLine("1.Register for a course");
            Console.WriteLine("2.Drop a course");
            Console.WriteLine("3.Show the completed student schedule");
            Console.WriteLine("4.LogOut");
            Console.Write("enter your choois:");
            try
            {
                x = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("invalid input");
                Console.ReadKey();
            }
           
            return x;
        }
        static int insfun()
        {
            int x=-1;
            Console.WriteLine("1.Show a student roster for a given course");
            Console.WriteLine("2.LogOut");
            Console.Write("enter your choois:");
            try
            {
                x = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("invalid input");
            }
            

            return x;
        }
        static void AddToFile(Object []obj, string file_name)
        {
            FileStream fs = new FileStream(file_name, FileMode.Append, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            for(int i=0;i<obj.Length;i++)
            {
                bf.Serialize(fs, obj[i]);
            }
            
            fs.Close();
        }
        static void Main(string[] args)
        {

            Console.SetWindowSize(160,50);
            //---------------------------------------------------------------------------------------------//
            //---------------------------------Creat course file if not exist -----------------------------//

            FileStream fscours = new FileStream("COURSE.txt", FileMode.Append, FileAccess.Write);
            fscours.Close();

            //---------------------------------------------------------------------------------------------//
            //---------------------------------------------------------------------------------------------//

            //---------------------------------------------------------------------------------------------//
            //---------------------------------Creat registrar object -------------------------------------//

            REGISTRAR rege = new REGISTRAR("reg", 123);

            //---------------------------------------------------------------------------------------------//
            //---------------------------------------------------------------------------------------------//


            //---------------------------------------------------------------------------------------------//
            //--------------Creat student object and save the object to STUDENT.txt file-------------------//

            STUDENT[] st = new STUDENT[100];
            st[0] = new STUDENT("Ali", 124, "CPE");
            st[1] = new STUDENT("Omar", 136, "CS");
            st[2] = new STUDENT("Maha", 147, "EE");
            st[3] = new STUDENT("Naser", 158, "CE");
            st[4] = new STUDENT("Samer", 169, "CIS");
            st[5] = new STUDENT("Fadi", 178, "PYS");
            st[6] = new STUDENT("Eman", 182, "SE");
            st[7] = new STUDENT("Yara", 194, "MED");

            if (!File.Exists("STUDENT.txt"))
            {

                FileStream fs = new FileStream("STUDENT.txt", FileMode.Create, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                for (int i = 0; i < 8; i++)
                {
                    bf.Serialize(fs, st[i]);
                }
                fs.Close();
            }
            //---------------------------------------------------------------------------------------------//
            //---------------------------------------------------------------------------------------------//


            //---------------------------------------------------------------------------------------------//
            //------------Creat instructor object and save the object to INSTRUCTOR.txt file---------------//
            INSTRUCTOR[] ins = new INSTRUCTOR[100];
            ins[0] = new INSTRUCTOR("Kamal", 225);
            ins[1] = new INSTRUCTOR("Basem", 249);
            ins[2] = new INSTRUCTOR("Waleed", 243);
            ins[3] = new INSTRUCTOR("Hamza", 278);
            if (!File.Exists("INSTRUCTOR.txt"))
            {
                FileStream fs2 = new FileStream("INSTRUCTOR.txt", FileMode.Create, FileAccess.Write);
                BinaryFormatter bf2 = new BinaryFormatter();
                for (int i = 0; i < 4; i++)
                {
                    bf2.Serialize(fs2, ins[i]);
                }
                fs2.Close();
            }
            //---------------------------------------------------------------------------------------------//
            //---------------------------------------------------------------------------------------------//


            int login_type = 0;
            while (login_type != 4)
            {
                Console.Clear();
                login_type = logintype();
                if (login_type == 1)
                {
                    int task = 0; string us=""; int id=-1;
                    
                    
                    try
                    {
                        Console.Write("enter user :");
                        us = Console.ReadLine();
                        Console.Write("enter ID :");
                        id = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("invalid input");
                        Console.ReadKey();
                    }

                    
                   
                   
                    if (rege.getID == id && rege.getUser == us)
                    {
                        while (task != 4)
                        {
                            Console.Clear();
                            Console.WriteLine("Welcome REGISTRAR");
                            task = regfun();
                            if (task == 1)
                            {
                                rege.Add_new_course();
                            }
                            else if (task == 2)
                            {
                                rege.Cancel_exiting_course();
                            }
                            else if (task == 3)
                            {
                                rege.Show_all_active_courses();
                            }
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("wrong id or user");
                    }
                }
                else if (login_type == 2)
                {
                    int task = 0;
                    int cc = 0;
                    FileStream student_file = new FileStream("STUDENT.txt", FileMode.Open, FileAccess.Read);
                    STUDENT[] student_list = new STUDENT[100];
                    try
                    {
                        BinaryFormatter student_binary = new BinaryFormatter();
                        while (student_file.Position < student_file.Length)
                        {
                            student_list[cc] = (STUDENT)student_binary.Deserialize(student_file);
                            cc++;
                        }
                    }
                    catch (SerializationException e)
                    {
                        Console.WriteLine(" Failed to serialize student. Reason: " + e.Message);
                        throw;
                    }
                    finally
                    {
                        student_file.Close();
                    }

                    string us = ""; int id = -1;  
                    try
                    {
                        Console.Write("enter user :");
                        us = Console.ReadLine();
                        Console.Write("enter ID :");
                        id = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("invalid input");
                        Console.ReadKey();
                    }
                    int k = cc+20;
                    for (int i = 0; i < cc; i++)
                    {
                        if (student_list[i].getID == id && student_list[i].getUser == us)
                        {
                            k = i;
                            break;
                        }
                    }
                    if (k < cc)
                    {


                        while (task != 4)
                        {
                            Console.Clear();
                            Console.WriteLine("Welcome " + student_list[k].getUser);
                            task = stdfun();
                            if (task == 1)
                            {
                                student_list[k].Register_for_a_course();
                            }
                            else if (task == 2)
                            {
                                student_list[k].Drop_a_course();
                            }
                            else if (task == 3)
                            {
                                student_list[k].Show_the_completed_student_schedule();
                            }
                            Console.ReadKey();
                        }
                    }
                    FileStream student_file_1 = new FileStream("STUDENT.txt", FileMode.Create, FileAccess.Write);
                    try
                    {
                        BinaryFormatter student_binary_1 = new BinaryFormatter();
                        for(int i=0;i<cc;i++)
                        {
                            student_binary_1.Serialize(student_file_1, student_list[i]); 
                        }
                    }
                    catch (SerializationException e)
                    {
                        Console.WriteLine(" Failed to serialize student. Reason: " + e.Message);
                        throw;
                    }
                    finally
                    {
                        student_file_1.Close();
                    }
                    

                }
                else if (login_type == 3)
                {

                    int task = 0;
                    int cc = 0;
                    FileStream instructor_file = new FileStream("INSTRUCTOR.txt", FileMode.Open, FileAccess.Read);
                    INSTRUCTOR[] instructor_list = new INSTRUCTOR[100];
                    try
                    {
                        BinaryFormatter instructor_binary = new BinaryFormatter();
                        while (instructor_file.Position < instructor_file.Length)
                        {
                            instructor_list[cc] = (INSTRUCTOR)instructor_binary.Deserialize(instructor_file);
                            cc++;
                        }
                    }
                    catch (SerializationException e)
                    {
                        Console.WriteLine(" Failed to deserialize instructor. Reason: " + e.Message);
                        throw;
                    }
                    finally
                    {
                        instructor_file.Close();
                    }

                    string us = ""; int id = -1;
                    try
                    {
                        Console.Write("enter user :");
                        us = Console.ReadLine();
                        Console.Write("enter ID :");
                        id = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("invalid input");
                        Console.ReadKey();
                    }
                    int k = 0;
                    for (; k < cc; k++)
                    {
                        if (instructor_list[k].getID == id && instructor_list[k].getUSER == us)
                        {
                            break;
                        }
                    }
                    if (k < cc)
                    {

                        while (task != 2)
                        {
                            Console.Clear();
                            Console.WriteLine("Welcome " + instructor_list[k].getUSER);
                            task = insfun();
                            if (task == 1)
                            {
                                instructor_list[k].Show_a_student_roster_for_a_given_course();
                            }
                            Console.ReadKey();
                        }
                    }
                    FileStream instructor_file_1 = new FileStream("INSTRUCTOR.txt", FileMode.Open, FileAccess.Write);
                    try
                    {
                        BinaryFormatter instructor_binary_1 = new BinaryFormatter();
                        for(int i=0;i<cc;i++)
                        {
                            instructor_binary_1.Serialize(instructor_file_1, instructor_list[i]);
                            
                        }
                    }
                    catch (SerializationException e)
                    {
                        Console.WriteLine(" Failed to serialize instructor. Reason: " + e.Message);
                        throw;
                    }
                    finally
                    {
                        instructor_file_1.Close();
                    }
                }
                //Console.ReadKey();
            }
            Console.WriteLine("good bye");
            Console.ReadKey();
        }
    }
}