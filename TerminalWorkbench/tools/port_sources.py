"""One-time, reviewable source port. Never run as part of an application build.

Only files explicitly included in the original project are read. Original files
and runtime data are never changed. Generated controllers retain operation and
reply handlers; their views are native WPF controls.
"""
from pathlib import Path
import re
import xml.etree.ElementTree as ET

BASE = Path(__file__).resolve().parents[2]
SOURCE = BASE / 'DtuTestManager'
OUT = BASE / 'TerminalWorkbench' / 'TerminalWorkbench.App' / 'Ported'
if OUT.exists() and any(OUT.rglob('*.cs')):
    raise SystemExit('The one-time port already exists. Refusing to overwrite reviewed migration sources.')
project = ET.parse(SOURCE / 'DtuTestManager.csproj')
compiled = [Path(e.attrib['Include'].replace('\\', '/')) for e in project.findall('.//{*}Compile')]

def save(file, code):
    dest = OUT / file
    dest.parent.mkdir(parents=True, exist_ok=True)
    dest.write_text(code, encoding='utf-8-sig', newline='\r\n')

def remove_method(code, signature):
    match = re.search(signature, code)
    if not match:
        return code
    start = code.index('{', match.end())
    level = 1
    end = start + 1
    while level:
        level += (code[end] == '{') - (code[end] == '}')
        end += 1
    return code[:match.start()] + code[end:]

for file in compiled:
    if file.name.endswith('.Designer.cs') or str(file).startswith(('Properties', 'MyControls')) or file.name == 'Program.cs':
        continue
    code = (SOURCE / file).read_text(encoding='utf-8-sig')
    code = code.replace('using System.Windows.Forms;', 'using TerminalWorkbench.Presentation;')
    code = re.sub(r'using (WeifenLuo.WinFormsUI.Docking|AxTeeChart|TeeChart);[^\n]*', '', code)
    code = code.replace('System.Windows.Forms.', 'TerminalWorkbench.Presentation.')
    code = code.replace('using System.Data.SQLite;', 'using Microsoft.Data.Sqlite;')
    code = code.replace('using System.Runtime.Serialization.Formatters.Binary;', '')
    code = code.replace('Encoding.Default', 'TerminalWorkbench.Presentation.TextEncoding.Ansi')
    code = code.replace('System.Text.TerminalWorkbench.', 'TerminalWorkbench.')
    code = code.replace('new System.Security.Cryptography.MD5CryptoServiceProvider()', 'System.Security.Cryptography.MD5.Create()')
    code = code.replace('new System.Security.Cryptography.SHA1Managed()', 'System.Security.Cryptography.SHA1.Create()')
    if file.name == 'CGlobal.cs':
        code = re.sub(r'\[DllImport\("User32.DLL"\)\]\s*public static extern int PostMessage\(int hWnd, int Msg, int wParam, int lParam\);',
            'public static int PostMessage(int hWnd, int Msg, int wParam, int lParam)\n        {\n            return UiMessages.Post(hWnd, Msg, wParam, lParam);\n        }', code)
        code = re.sub(r'\[DllImport\("User32.DLL"\)\]\s*public static extern int SendMessage\(int hWnd, int Msg, int wParam, int lParam\);',
            'public static int SendMessage(int hWnd, int Msg, int wParam, int lParam)\n        {\n            return UiMessages.Send(hWnd, Msg, wParam, lParam);\n        }', code)
        code = remove_method(code, r'        public static T CloneObjectEx<T>\(T o\)')
    if file.name == 'CTransTerminal.cs':
        code = code.replace('CGlobal.CloneObjectEx(CTransGlobal.m_DefaultTermValueList)', 'ModelCopies.CopyValues(CTransGlobal.m_DefaultTermValueList)')
        code = code.replace('CGlobal.CloneObjectEx(pBasicTerm.m_ValueList)', 'ModelCopies.CopyValues(pBasicTerm.m_ValueList)')
        code = code.replace('CGlobal.CloneObjectEx(pBasicTerm.m_TermDataValue)', 'ModelCopies.CopyTerminalData(pBasicTerm.m_TermDataValue)')
    if file.name == 'CHisRecordForm.cs':
        code = code.replace('private bool bReadThread', 'private volatile bool bReadThread')
        code = code.replace('m_ReadFileThread.Abort();', 'm_ReadFileThread.Join();')
    if file.name == 'CRealCtrlForm.cs':
        # WPF owns compositing; the old reflection and native buffer toggles
        # neither apply nor belong in the presentation controller.
        code = remove_method(code, r'        private void DoubleBuffer\(Control control\)')
        code = code.replace('DoubleBuffer(this);', '')
        code = re.sub(r'            this\.SetStyle\([^\n]*\);\n', '', code)
        code = code.replace('            this.UpdateStyles();', '')
    if file.name == 'CYKForm.cs':
        code = re.sub(r'            typeof\(ListView\)\.GetProperty\("DoubleBuffered",[\s\S]+?\.SetValue\(LV_YK, true, null\);', '', code)
    if file.name == 'MainForm.cs':
        code = code.replace('System.Environment.Exit(0);', '')
        code = code.replace('            CGlobal.m_SecretEncode.CreateSecretDevice();', '')
        code = code.replace('            GetDevModuleFile("工程模板");', '            if (Directory.Exists("工程模板")) GetDevModuleFile("工程模板");')
    save(file, code)

properties = {'Text','Name','Dock','Location','Size','ClientSize','Enabled','Visible',
              'Checked','CheckOnClick','ReadOnly','Multiline','PasswordChar','MaxLength',
              'Interval','Minimum','Maximum','Step','Value','CheckBoxes','MultiSelect',
              'SelectedIndex','SplitterDistance','Panel1Collapsed','Panel2Collapsed',
              'ColumnCount','RowCount','Orientation','Anchor','ContextMenuStrip','TabIndex',
              'DropDownStyle','Format','CustomFormat','BackColor','ForeColor','AutoSize',
              'TextAlign','HeaderText','DataPropertyName','Width','Height','MaxDropDownItems'}

for file in compiled:
    if not file.name.endswith('.Designer.cs') or 'Forms' not in str(file):
        continue
    design = (SOURCE / file).read_text(encoding='utf-8-sig')
    class_name = re.search(r'partial\s+class\s+(\w+)', design).group(1)
    fields = re.findall(r'        (?:private|public|internal)\s+([\w.]+)\s+(\w+)\s*;', design)
    fields = [(t.split('.')[-1],n) for t,n in fields if n != 'components']
    lines = ['using System;', 'using System.Drawing;', 'using TerminalWorkbench.Presentation;',
             'namespace DtuTestManager', '{', f'    partial class {class_name}', '    {']
    lines.extend(f'        private {typ} {name};' for typ,name in fields)
    lines += ['        private void InitializeComponent()', '        {']
    lines.extend(f'            {name} = new {typ}();' for typ,name in fields)
    # Values of controls are set before event handlers are attached, matching
    # designer initialization without accidentally firing device operations.
    assignments = re.findall(r'^[ \t]*(this(?:\.[\w]+){1,3})\s*=\s*([^;\n]+);', design, re.M)
    for left,right in assignments:
        prop = left.split('.')[-1]
        if prop not in properties or 'resources.' in right:
            continue
        right = right.replace('System.Windows.Forms.', '')
        if prop=='Anchor':
            continue
        lines.append(f'            {left} = {right};')
    # Designer collection calls are simple and use already-created fields.
    for match in re.finditer(r'(this(?:\.[\w]+){0,3}\.(?:Controls|Items|DropDownItems|Columns)\.Add(?:Range)?)\(([\s\S]*?)\);',design):
        call,args = match.groups()
        args = args.replace('System.Windows.Forms.', '').replace('DtuTestManager.', '')
        lines.append(f'            {call}({args});')
    for owner,event,handler in re.findall(r'this\.(\w+)\.(\w+)\s*\+=\s*new\s+[\w.]+\(this\.(\w+)\)', design):
        lines.append(f'            {owner}.{event} += {handler};')
    for event,handler in re.findall(r'this\.(\w+)\s*\+=\s*new\s+[\w.]+\(this\.(\w+)\)', design):
        lines.append(f'            {event} += {handler};')
    lines += ['        }', '    }', '}']
    save(file.with_name(file.name.replace('.Designer.cs','.View.cs')), '\n'.join(lines)+'\n')
print('Ported compiled domain sources and all screen event handlers. Review output before building.')
