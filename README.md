# PasswordLab

PasswordLab is a command-line tool for encrypting and decrypting files using AES encryption. It provides a simple interface to secure your files with a password.

## Table of Contents

- [PasswordLab](#passwordlab)
  - [Table of Contents](#table-of-contents)
  - [Installation](#installation)
    - [Prerequisites](#prerequisites)
    - [Installing](#installing)
  - [Usage](#usage)
    - [Encrypt a File](#encrypt-a-file)
    - [Encrypt a Folder](#encrypt-a-folder)
    - [Encrypt Multiple Components](#encrypt-multiple-components)
  - [CLI Commands](#cli-commands)

## Installation

### Prerequisites

- .NET SDK 8.0 or later

### Installing

1. Install via nuget.org
```sh 
dotnet tool install --global PasswordLab --version "version"
```

## Usage

### Encrypt a File
To encrypt a file, use the command ```encrypt``` with the ```-f``` option to specify the file path, ```-o``` to specify the output path, ```-n``` to specify the output file name, and ```--password``` to specify the password.

```sh
passwordlab encrypt -f "file-path-to-encrypt" -n "encrypted-file-name" --password "password"
```

### Encrypt a Folder
To encrypt a folder, just copy the path of the directory and specify it in ```-f``` option.
```sh
passwordlab encrypt -f "folder-path-to-encrypt" -o"output-path" -n "encrypted-file-name" --password "password"
```

### Encrypt Multiple Components
To encrypt a multiple files and folders, specify each path using ```-f``` option.
```sh
passwordlab encrypt -f "folder-path-1" "file-path-2" "folder-path-3" -o "output-path" -n "encrypted-file-name" --password "password"
```

## CLI Commands

| Command       | Description                                                                 | Example                                                                                     |
|---------------|-----------------------------------------------------------------------------|---------------------------------------------------------------------------------------------|
| `encrypt`     | Encrypt a file, folder, or multiple components                              | `passwordlab encrypt -f "file-path" -o "output-path(optional)" -n "encrypted-file-name" --password "password"` |
| `decrypt`     | Decrypt a file, folder, or multiple components                              | `passwordlab decrypt -f "file-path" -o "output-path(optional)" --password "password"`       |
| `help`        | Display help information for the commands                                   | `passwordlab --help`                                                                        |
| `version`     | Display version information.                                                | `passwordlab --version`                                                                     |