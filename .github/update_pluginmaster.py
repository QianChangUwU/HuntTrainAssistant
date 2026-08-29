import json
import os
import sys
import time

repo_path = sys.argv[1]
version = sys.argv[2]
repo_full_name = sys.argv[3]

json_path = os.path.join(repo_path, 'pluginmaster.json')

with open(json_path, 'r', encoding='utf-8') as f:
    data = json.load(f)

download_url = f"https://github.com/{repo_full_name}/releases/download/v{version}/latest.zip"

entry = next((x for x in data if x.get('InternalName') == 'HuntTrainAssistant'), {})

entry['Name'] = 'HuntTrainAssistant'
entry['Author'] = 'NightmareXIV'
entry['Punchline'] = 'Hunt train assistance tool.'
entry['Description'] = 'Provides useful tools for hunt trains such as: highlighting conductor messages and muting all other chat, auto-opening flags from conductors and auto-teleporting to announced destination.'
entry['InternalName'] = 'HuntTrainAssistant'
entry['AssemblyVersion'] = version
entry['TestingAssemblyVersion'] = version
entry['DalamudApiLevel'] = 15
entry['TestingDalamudApiLevel'] = 15
entry['DownloadLinkInstall'] = download_url
entry['DownloadLinkUpdate'] = download_url
entry['DownloadLinkTesting'] = download_url
entry['RepoUrl'] = f'https://github.com/{repo_full_name}'
entry['Tags'] = ['hunt', 'train']
entry['ApplicableVersion'] = 'any'
entry['LoadPriority'] = 0
entry['AcceptsFeedback'] = True
entry['LastUpdate'] = int(time.time())

if entry not in data:
    data.append(entry)

with open(json_path, 'w', encoding='utf-8') as f:
    json.dump(data, f, indent=4, ensure_ascii=False)
