using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Excel;  // 确保引用了正确的 Excel 读取库
using System.Data;  // 确保引用了正确的 DataSet 和 DataTable 库

public class MyEditor
{
    // 在Unity编辑器菜单中添加一个菜单项，用于将Excel转换为TXT文件
    [MenuItem("我的工具/excel转成txt")]
    public static void ExportExcelToTxt()
    {
        // 获取Excel文件存放的路径
        string assetPath = Application.dataPath + "/_Excel";
        // 获取路径下所有的.xlsx文件
        string[] files = Directory.GetFiles(assetPath, "*.xlsx");

        // 遍历每一个Excel文件
        for (int i = 0; i < files.Length; i++)
        {
            // 替换路径中的反斜杠为正斜杠
            files[i] = files[i].Replace("\\", "/");

            // 打开Excel文件
            using (FileStream fs = File.Open(files[i], FileMode.Open, FileAccess.Read))
            {
                // 使用Excel读取器读取Excel文件
                var excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                // 将Excel数据读取到DataSet中
                DataSet dataSet = excelDataReader.AsDataSet();
                // 获取第一个表格
                DataTable table = dataSet.Tables[0];
                // 将表格数据读取并写入TXT文件
                readTableToTxt(files[i], table);
            }
        }

        // 刷新资产数据库，使新生成的TXT文件显示在Unity中
        AssetDatabase.Refresh();
    }

    // 读取DataTable数据并写入TXT文件
    private static void readTableToTxt(string filePath, DataTable table)
    {
        // 获取文件名，不包括扩展名
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        // 构建TXT文件的存放路径
        string path = Application.dataPath + "/Resources/Data/" + fileName + ".txt";

        // 如果文件已存在，删除旧文件
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        // 创建文件流，准备写入数据
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            // 使用StreamWriter写入数据
            using (StreamWriter sw = new StreamWriter(fs))
            {
                // 遍历表格的每一行
                for (int row = 0; row < table.Rows.Count; row++)
                {
                    // 获取当前行数据
                    DataRow dataRow = table.Rows[row];
                    string str = "";

                    // 遍历当前行的每一列
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        // 获取当前单元格的数据并转换为字符串
                        string val = dataRow[col].ToString();
                        // 将当前单元格的数据添加到行字符串中，使用制表符分隔
                        str = str + val + "\t";
                    }

                    // 写入当前行数据
                    sw.Write(str);

                    // 如果当前行不是最后一行，写入换行符
                    if (row != table.Rows.Count - 1)
                    {
                        sw.WriteLine();
                    }
                }
            }
        }
    }
}
