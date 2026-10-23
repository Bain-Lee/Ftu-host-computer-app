"""Inventory only explicitly compiled legacy screens and their actions."""
from pathlib import Path
import json
import re
import xml.etree.ElementTree as ET

BASE = Path(__file__).resolve().parents[2]
SOURCE = BASE / 'DtuTestManager'
OUT = BASE / 'TerminalWorkbench'
project = ET.parse(SOURCE / 'DtuTestManager.csproj')
compiled = [Path(e.attrib['Include'].replace('\\', '/')) for e in project.findall('.//{*}Compile')]
screens = []
for file in compiled:
    if 'Forms' not in str(file) or file.name.endswith('.Designer.cs'):
        continue
    code = (SOURCE / file).read_text(encoding='utf-8-sig')
    class_name = re.search(r'partial\s+class\s+(\w+)', code).group(1)
    designer = (SOURCE / file.with_suffix('.Designer.cs')).read_text(encoding='utf-8-sig')
    title = re.search(r'this\.Text\s*=\s*"([^"\r\n]*)";', designer)
    handlers = re.findall(r'this\.(\w+)\.(\w+)\s*\+=\s*new\s+[\w.]+\(this\.(\w+)\)', designer)
    lifecycle = re.findall(r'this\.(\w+)\s*\+=\s*new\s+[\w.]+\(this\.(\w+)\)', designer)
    tasks = sorted(set(int(n) for n in re.findall(r'SendTaskOrder\(\s*(\d+)\s*\)', code)))
    screens.append(dict(source=str(file).replace('\\', '/'), type=class_name,
                        title=title.group(1) if title else class_name,
                        handlers=[dict(control=c, event=e, handler=h) for c, e, h in handlers],
                        lifecycle=[dict(event=e, handler=h) for e, h in lifecycle],
                        taskNumbers=tasks, implementation='pending', offlineValidation='pending', deviceValidation='pending'))
(OUT / 'FEATURE_INVENTORY.json').write_text(json.dumps(screens, ensure_ascii=False, indent=2)+'\n', encoding='utf-8')
print(f'{len(screens)} compiled screens; {sum(len(s["handlers"]) for s in screens)} control event bindings.')
