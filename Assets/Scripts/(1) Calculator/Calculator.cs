using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Calculator
{
    public class Calculator : MonoBehaviour
    {
        [Header("Variables")]
        [SerializeField] private TMP_InputField m_inputFieldText;
        [SerializeField] private TMP_Text m_inputResult;

        private Stack<double> m_allNumbers = new Stack<double>(); // numbers in stack
        private bool m_isError;
        private int m_operationsCount;

        [Header("Debug")]
        [SerializeField] private double m_number; // reusable variable
        [SerializeField] private string m_args; // reusable variable
        [SerializeField] private string[] m_argsList; // reusable variable

#region === OPERATIONS ===
        private const string TEXT_PLUS = "+";
        private const string TEXT_MINUS = "-";
        private const string TEXT_MULTI = "*";
        private const string TEXT_DIVISION = "/";
        private const string TEXT_POW = "^";
        private const string TEXT_MODULO = "%";
        private const string TEXT_SQRT = "&";
        private const string TEXT_PERCENT = "#";
#endregion

#region === CONSTANTS ERRORS TEXT ===
        private const string TEXT_ERROR = "Ошибка: неизвестный символ!";
        private const string TEXT_NO_OPERATION_ERROR = "Ошибка: не указана ни одна операция!";
        private const string TEXT_DIVIDE_ZERO_ERROR = "Ошибка: деление на ноль!";
        private const string TEXT_NO_ARGS_ERROR = "Ошибка: недостаточно аргументов для операции!";
        private const string TEXT_SQRT_ERROR = "Ошибка: число должно быть положительное и больше 0!";
#endregion

#region === DO SOME CODE ===
        public void Calculate()
        {
            m_args = m_inputFieldText.text;
            m_argsList = m_args.Split(' ');

            for (int i = 0; i < m_argsList.Length; i++)
            {
                if (double.TryParse(m_argsList[i], out m_number))
                {
                    m_allNumbers.Push(m_number);
                    continue;
                }

                m_operationsCount++;
                switch(m_argsList[i])
                {
                    case TEXT_PLUS:
                        if (ErrorNoArguments(2)) break;

                        m_allNumbers.Push(m_allNumbers.Pop() + m_allNumbers.Pop());
                        break;

                    case TEXT_MINUS:
                        if (ErrorNoArguments(2)) break;

                        m_number = m_allNumbers.Pop();
                        m_allNumbers.Push(m_allNumbers.Pop() - m_number);
                        break;

                    case TEXT_MULTI:
                        if (ErrorNoArguments(2)) break;

                        m_allNumbers.Push(m_allNumbers.Pop() * m_allNumbers.Pop());
                        break;

                    case TEXT_DIVISION:
                        if (ErrorNoArguments(2)) break;
                        if (ErrorDivideZero(m_allNumbers.Peek())) break;

                        m_allNumbers.Push(m_allNumbers.Pop() / m_allNumbers.Pop());
                        break;

                    case TEXT_POW:
                        if (ErrorNoArguments(2)) break;

                        m_allNumbers.Push(Math.Pow(m_allNumbers.Pop(), m_allNumbers.Pop()));
//                      Да, я знаю про Mathf, но в данном случае Unity не предоставляет реализацию для типа double + задание изначально для NET платформы
                        break;

                    case TEXT_MODULO:
                        if (ErrorNoArguments(2)) break;

                        m_allNumbers.Push(m_allNumbers.Pop() % m_allNumbers.Pop());
                        break;

                    case TEXT_SQRT:
                        if (ErrorNoArguments(1)) break;
                        if (ErrorSqrt(m_allNumbers.Peek())) break;

                        m_allNumbers.Push(Math.Sqrt(m_allNumbers.Pop()));
                        break;

                    case TEXT_PERCENT:
                        if (ErrorDivideZero(m_allNumbers.Peek())) break;

                        m_allNumbers.Push((m_allNumbers.Pop() / 100) * m_allNumbers.Pop());
                        break;

                    default:
                        m_isError = true;
                        m_operationsCount--;

                        if (m_inputFieldText.text == string.Empty)
                        {
                            m_inputResult.text = string.Empty;
                            return;
                        }

                        m_inputResult.text = TEXT_ERROR;
                        return;
                }
            }

            if (ErrorNoOperations())
            {
                ClearData();
                return;
            }

//          Result:
            if (!m_isError && m_allNumbers.Count > 0)
            {
                m_inputResult.text = m_allNumbers.Pop().ToString();
                ClearData();

                return;
            }

            ClearData();
        }

        private bool ErrorNoArguments(int arg)
        {
            if (m_allNumbers.Count < arg)
            {
                m_isError = true;
                m_inputResult.text = TEXT_NO_ARGS_ERROR;
                return true;
            }

            return false;
        }

        private bool ErrorNoOperations()
        {
            if (m_operationsCount == 0)
            {
                m_isError = true;
                m_inputResult.text = TEXT_NO_OPERATION_ERROR;
                return true;
            }

            return false;
        }

        private bool ErrorSqrt(double number)
        {
            if (number <= 0.0d)
            {
                m_isError = true;
                m_inputResult.text = TEXT_SQRT_ERROR;
                return true;
            }

            return false;
        }

        private bool ErrorDivideZero(double number)
        {
            if (number == 0.0d)
            {
                m_isError = true;
                m_inputResult.text = TEXT_DIVIDE_ZERO_ERROR;
                return true;
            }

            return false;
        }

        private void ClearData()
        {
            m_isError = false;
            m_operationsCount = 0;
            m_allNumbers.Clear();
        }
    }
#endregion
}
