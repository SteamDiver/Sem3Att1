using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace Utils
{
    public class DataGridViewUtils
    {
        // Инициализация DataGridView для работы с массивами (различные настройки и обработчики событий);
        // при добавление кнопок управления кол-вом строк и столбцов уменьшает размеры DataGridView
        public static void InitGridForArr(DataGridView dgv, 
                int defaultColWidth, bool readOnly,
                bool showRowsIndexes, bool showColsIndexes,
                bool changeRowsCountButtons, bool changeColsCountButtons,
                int changeButtonsSize = 22, int changeButtonsMargin = 6) {
            List<Button> buttons = new List<Button>();

            int shiftSize = changeButtonsSize + changeButtonsMargin;
            if (changeRowsCountButtons) {
                // -
                Button minusButton = new Button {
                    Width = changeButtonsSize,
                    Height = changeButtonsSize,
                    Left = dgv.Left,
                    Top = dgv.Top + (changeColsCountButtons ? shiftSize : 0),
                    Text = "\u2014",  // длинное тире (один символ Unicode с кодом 0x2014)
                    Parent = dgv.Parent,
                    Name = dgv.Name + "_MinusRowButton",
                };
                minusButton.Click += (sender, e) => {
                    if (dgv.RowCount > 1)
                        dgv.RowCount--;
                };
                buttons.Add(minusButton);
                // +
                Button plusButton = new Button {
                    Width = changeButtonsSize,
                    Height = changeButtonsSize,
                    Left = minusButton.Left,
                    Top = minusButton.Top + shiftSize,
                    Text = "+",
                    Parent = dgv.Parent,
                    Name = dgv.Name + "_PlusRowButton",
                };
                plusButton.Click += (sender, e) => {
                    dgv.RowCount++;
                };
                buttons.Add(plusButton);

                // уменьшение размера и сдвиг грида
                dgv.Width -= shiftSize;
                dgv.Left += shiftSize;

            }
            if (changeColsCountButtons) {
                // -
                Button minusButton = new Button {
                    Width = changeButtonsSize,
                    Height = changeButtonsSize,
                    Left = dgv.Left,
                    Top = dgv.Top,
                    Text = "\u2014",  // длинное тире (один символ Unicode с кодом 0x2014)
                    Parent = dgv.Parent,
                    Name = dgv.Name + "_MinusColButton",
                };
                minusButton.Click += (sender, e) => {
                    if (dgv.ColumnCount > 1)
                        dgv.ColumnCount--;
                };
                buttons.Add(minusButton);
                // +
                Button plusButton = new Button {
                    Width = changeButtonsSize,
                    Height = changeButtonsSize,
                    Left = minusButton.Left + shiftSize,
                    Top = minusButton.Top,
                    Text = "+",
                    Parent = dgv.Parent,
                    Name = dgv.Name + "_PlusColButton",
                };
                plusButton.Click += (sender, e) => {
                    dgv.ColumnCount++;
                };
                buttons.Add(plusButton);

                // уменьшение размера грида и сдвиг кнопок
                dgv.Height -= minusButton.Height + changeButtonsMargin;
                dgv.Top += minusButton.Height + changeButtonsMargin;
            }

            // запрет добавления новых строк
            dgv.AllowUserToAddRows = false;
            // запрет удаления строк
            dgv.AllowUserToDeleteRows = false;
            // запрет менять столбцы местами
            dgv.AllowUserToOrderColumns = false;
            // запрет изменять ширину столбцов
            dgv.AllowUserToResizeColumns = false;
            // запрет изменять ширину строк
            dgv.AllowUserToResizeRows = false;
            // запрет менять ширину заголовка строк (серые ячейки)
            dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            // запрет менять ширину заголовка столбцов (серые ячейки)
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // разрешаем прокрутку содержимого (на всякий случай)
            dgv.ScrollBars = ScrollBars.Both;

            // установить ширину заголовка строк в 65 пикселей
            dgv.RowHeadersWidth = defaultColWidth;
            // установить высоту заголовка столбцов равной высоте строки по умолчанию
            dgv.ColumnHeadersHeight = dgv.RowTemplate.Height;

            dgv.RowHeadersVisible = showRowsIndexes;
            dgv.ColumnHeadersVisible = showColsIndexes;
            dgv.ReadOnly = readOnly;

            // делегат (анонимный метод), который настраивает строки и столбцы
            // (задает заголовки и, где надо, размеры)
            Action updateHeaders = () => {
                if (showRowsIndexes)
                    for (int r = 0; r < dgv.RowCount; r++)
                        dgv.Rows[r].HeaderCell.Value = string.Format("[ {0} ]", r);
                /*if (showColsIndexes)
                    for (int c = 0; c < dgv.ColumnCount; c++) {
                        DataGridViewColumn column = dgv.Columns[c];
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                        column.HeaderCell.Value = string.Format("[ {0} ]", c);
                    }*/
            };

            // привязываем обработчик события добавления столбца, чтобы изменять его размер
            dgv.ColumnAdded += (sender, e) => {
                e.Column.Width = defaultColWidth;
                e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
                updateHeaders();
            };
            dgv.RowsAdded += (sender, e) => {
                updateHeaders();    
            };

            // привязываем обработчик событий, который очищает выделенные ячейки по клавише delete
            dgv.PreviewKeyDown += (sender, e) => {
                if (dgv.Enabled && !dgv.ReadOnly && e.KeyValue == 46)
                    foreach (var cell in dgv.SelectedCells)
                        ((DataGridViewCell) cell).Value = null;
            };

            // обработчик событий, который активирует и дективирует кнопки в зависимости от состояния dgv
            EventHandler eh = (sender, e) => {
                foreach (Button b in buttons)
                    b.Enabled = dgv.Enabled && !dgv.ReadOnly;
            };
            // привязываем созданный выше обработчик к событиям изменения свойств Enabled и ReadOnly DataGridView
            dgv.EnabledChanged += eh;
            dgv.ReadOnlyChanged += eh;
            eh(dgv, EventArgs.Empty);

            // привязываем обработчик событий, который меняет выравнивание в ячейках в зависимости от содержимого
            // (целые числа - выравнивание вправо, иначе - влево)
            dgv.CellValidated += (sender, e) => {
                DataGridViewCell cell = dgv[e.ColumnIndex, e.RowIndex];
                int temp;
                // выравнивание (если конвертится в int - по правому краю, иначе - по левому)
                cell.Style.Alignment =
                    int.TryParse("" + cell.Value, out temp) ? DataGridViewContentAlignment.MiddleRight : DataGridViewContentAlignment.MiddleLeft;
            };

            // привязываем обработчик событий, который нужным образом отрисовывает содержимое ячеек заголовков
            // (чтобы не рисовались всякие стрелки-звездочки и умещался весь текст)
            StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, };
            dgv.CellPainting += (sender, e) => {
                if (e.ColumnIndex < 0 || e.RowIndex < 0) {
                    e.PaintBackground(e.CellBounds, false);
                    if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
                        e.Graphics.DrawString(
                            (e.ColumnIndex < 0 ? dgv.Rows[e.RowIndex].HeaderCell.Value : dgv.Columns[e.ColumnIndex].HeaderCell.Value).ToString(),
                            dgv.RowHeadersDefaultCellStyle.Font,
                            new SolidBrush(dgv.RowHeadersDefaultCellStyle.ForeColor),
                            e.CellBounds,
                            sf
                        );
                    e.Handled = true;
                }
            };

            // установка минимального кол-ва столбцов и строк
            // (обязательно после привязки обработчика события добавления столбца)
            if (dgv.ColumnCount == 0)
                dgv.ColumnCount = 1;
            if (dgv.RowCount == 0)
                dgv.RowCount = 1;
        }


        // Запись данных из массива (одномерного или двухмерного) в DataGridView
        // (основная реализация, закрытый метод, используется в ArrayToGrid и Array2ToGrid)
        private static void ArrayToGridInner<T>(DataGridView dgv, Array data) {
            // выравнивание (если T == int - по правому краю, иначе - по-умолчанию)
            dgv.DefaultCellStyle.Alignment =
                typeof(T) == typeof(int) ? DataGridViewContentAlignment.MiddleRight : dgv.DefaultCellStyle.Alignment;

            int rowCount = data.Rank == 1 ? 1 : data.GetLength(0),
                colCount = data.Rank == 1 ? data.GetLength(0) : data.GetLength(1);

            DataGridViewSelectionMode originalSelectionMode = dgv.SelectionMode;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv.RowCount = rowCount;
            dgv.ColumnCount = colCount;
            dgv.SelectionMode = originalSelectionMode;

            for (int r = 0; r < rowCount; r++)
                for (int c = 0; c < colCount; c++)
                    dgv[c, r].Value = data.Rank == 1 ? data.GetValue(c) : data.GetValue(r, c);
        }

        // Запись данных из одномерного массива в DataGridView
        public static void ArrayToGrid<T>(DataGridView dgv, T[] data) {
            ArrayToGridInner<T>(dgv, data);
        }

        // Запись данных из списка в DataGridView
        public static void ListToGrid<T>(DataGridView dgv, IList<T> data) {
            ArrayToGridInner<T>(dgv, data.ToArray());
        }

        // Запись данных из двухмерного массива в DataGridView
        public static void ArrayToGrid<T>(DataGridView dgv, T[,] data) {
            ArrayToGridInner<T>(dgv, data);
        }

        // Запись данных из двухмерного массива в DataGridView
        public static void Array2ToGrid<T>(DataGridView dgv, T[,] data) {
            ArrayToGridInner<T>(dgv, data);
        }


        // Создание массива (одномерного или array2 - двумерного) из данных в DataGridView
        // (основная реализация, закрытый метод, используется в GridToArray и GridToArray2)
        private static Array GridToArrayInner<T>(DataGridView dgv, bool array2 = false) {
            Array result = array2 ? (Array) new T[dgv.RowCount, dgv.ColumnCount] : (Array) new T[dgv.ColumnCount];

            for (int r = 0; r < (array2 ? dgv.RowCount : 1); r++)
                for (int c = 0; c < dgv.ColumnCount; c++) {
                    object value = dgv[c, r].Value;
                    try {
                        value = (T) Convert.ChangeType(value, typeof(T));
                        if (array2)
                            result.SetValue(value, r, c);
                        else
                            result.SetValue(value, c);
                    }
                    catch (Exception except) {
                        throw new ApplicationException(
                            string.Format(
                                "Невозможно преобразовать в {2} значение \"{0}\" (в ячейке [{1}])",
                                value, (array2 ? r + ", " : "") + c, typeof(T).Name
                            ),
                            except
                        );
                    }
                }

            return result;
        }

        // Создание одномерного массива из данных в DataGridView
        public static T[] GridToArray<T>(DataGridView dgv) {
            return (T[]) GridToArrayInner<T>(dgv);
        }

        // Создание списка из данных в DataGridView
        public static List<T> GridToList<T>(DataGridView dgv) {
            return new List<T>(GridToArray<T>(dgv));
        }

        // Создание двухмерного массива из данных в DataGridView
        public static T[,] GridToArray2<T>(DataGridView dgv) {
            return (T[,]) GridToArrayInner<T>(dgv, true);
        }
    }
}
