using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Utilities
{
    // Logger.GetInstance().AddViewPort(new TypeViewPortLogger(ETypeViewPortLogger.Label, lblStatus));

    public enum ETypeViewPortLogger
    {
        Non = 0,
        MesageBox,
        File,
        Label,
        TextBox,
    }

    public enum ETypeViewPortLogger_Exception
    {
        All = 0,
        MesageBox,
        File
    }

    public struct TypeViewPortLogger
    {
        public ETypeViewPortLogger TypeViewPort;
        public Control ViewPort;

        public TypeViewPortLogger(ETypeViewPortLogger TypeViewPort = ETypeViewPortLogger.Non, Control ViewPort = null) {
            this.TypeViewPort = TypeViewPort;
            this.ViewPort = ViewPort;
        }
    }

    public class Logger
    {
        private List<TypeViewPortLogger> _viewPorts;
        private string _pathFileNameLogger;

        private static Logger INSTANCE;

        public string PathFileNameLogger => _pathFileNameLogger;

        private Logger()
        {
            _viewPorts = new List<TypeViewPortLogger>();
            _pathFileNameLogger = "..\\..\\Log\\LogFile.txt";
        }

        public static Logger GetInstance()
        {
            if (INSTANCE == null)
                INSTANCE = new Logger();
            return INSTANCE;
        }

        /// <summary>
        /// Добавить порт вывода
        /// </summary>
        /// <param name="newViewPort"> - добавляемый порт вывода</param>
        public void AddViewPort(TypeViewPortLogger newViewPort)
        {
            _viewPorts.Add(newViewPort);
        }

        /// <summary>
        /// Удалить порт вывода по индексу 
        /// </summary>
        /// <param name="index"> - индекс удаляемого порта</param>
        public void DeleteViewPortAt(int index)
        {
            _viewPorts.RemoveAt(index);
        }

        /// <summary>
        /// Очистить список портов ввывода
        /// </summary>
        public void ClearViewPorts()
        {
            _viewPorts.Clear();
        }

        /// <summary>
        /// Вывести уведомление во все доступные окна просмотра 
        /// </summary>
        /// <param name="message"> - сообщение</param>
        /// <param name="header"> - заголовок</param>
        public void Notify(string message, string header = "Внимание!")
        {
            try
            {
                if(_viewPorts.Count == 0)
                {
                    ViewDefaultPort(message, header);
                    return;
                }

                foreach(TypeViewPortLogger port in _viewPorts)
                {
                    switch (port.TypeViewPort)
                    {
                        case ETypeViewPortLogger.Label:
                            Label l = (Label)port.ViewPort;
                            l.Text = message;
                            break;

                        case ETypeViewPortLogger.TextBox:
                            TextBox t = (TextBox)port.ViewPort;
                            t.Text = message;
                            break;

                        case ETypeViewPortLogger.MesageBox:
                            MessageBox.Show(message, header);
                            break;

                        case ETypeViewPortLogger.File:
                            WriteToLogFile(message);
                            break;

                        default:
                            throw new Exception("Logger.Notify: Не удалось определить TypeViewPortLogger!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Вывести уведомление только в порт UI приложения
        /// </summary>
        /// <param name="message"> - сообщение</param>
        /// <param name="header"> - заголовок</param>
        public void Notify_UI(string message, string header = "Внимание!")
        {
            try
            {
                if (_viewPorts.Count == 0)
                {
                    ViewDefaultPort(message, header);
                    return;
                }

                foreach (TypeViewPortLogger port in _viewPorts)
                {
                    switch (port.TypeViewPort)
                    {
                        case ETypeViewPortLogger.Label:
                            Label l = (Label)port.ViewPort;
                            l.Text = message;
                            break;

                        case ETypeViewPortLogger.TextBox:
                            TextBox t = (TextBox)port.ViewPort;
                            t.Text = message;
                            break;

                        case ETypeViewPortLogger.MesageBox:
                            MessageBox.Show(message, header);
                            break;

                        default:
                            throw new Exception("Logger.Notify: Не удалось определить TypeViewPortLogger!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Вывод в порт вывода по умолчанию 
        /// </summary>
        /// <param name="message"> - сообщение</param>
        /// <param name="header"> - заголовок</param>
        public void Notify_DefaultPort(string message, string header = "Внимание!")
        {
            ViewDefaultPort(message, header);
        }

        /// <summary>
        /// Вывод исключению в стандарный порт (MessageBox) и файл лога
        /// </summary>
        /// <param name="message"> - сообщение</param>
        /// <param name="isToFile"> - записывать ли в файл</param>
        /// <param name="header"> - заголовок</param>
        public void Notify_Exception(string message, string header = "Вызвано исключение!", ETypeViewPortLogger_Exception port = ETypeViewPortLogger_Exception.All)
        {
            if (port == ETypeViewPortLogger_Exception.All || port == ETypeViewPortLogger_Exception.File)
                ViewDefaultPort(message, header);

            else if (port == ETypeViewPortLogger_Exception.All || port == ETypeViewPortLogger_Exception.File)
                WriteToLogFile(message);

            else
                throw new Exception("Ни один порт вывода исключения не задействован!");
        }

        /// <summary>
        /// Вывести уведомление только в порт файла логгера
        /// </summary>
        /// <param name="message"> - сообщение</param>
        public void Notify_File(string message)
        {
            try
            {
                WriteToLogFile(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ViewDefaultPort(string message, string header)
        {
            MessageBox.Show(message, header);
        }

        private void WriteToLogFile(string message)
        {
            try
            {
                string str = $"{DateTime.Now.ToString()}: {message}";

                using (StreamWriter writer = new StreamWriter(_pathFileNameLogger, true))
                {
                    writer.WriteLine(str);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
    }
}
