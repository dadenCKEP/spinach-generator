# spinach-generator
hou-ren-souを生成、ではなく日報や週報の作成を補助するツール。

## テンプレート
XMLライクなテンプレートを作成する必要がある。
```XML
件名:
日報(<date />: <name />)

本文:
#日報 <date />
<h_today>## 1. 本⽇の活動報告</h_today>
<today />

<h_tomorrow>## 2. 翌⽇の予定</h_tomorrow>

<h_other>## 3. その他</h_other>

以上
```

### 凡例
#### 挿入位置指定タグ
* `<date />`  
  作成時の日付が`yyyy-MM-dd`形式で挿入される。
* `<name />`  
  初期設定時に入力した名前が挿入される。
* `<today />`  
  テンプレート内で、前日の日報内の`</h_tomorrow>`から`<h_other>`が挿入される位置。

#### 構文解析用タグ
* `<h_today>`～`</h_today>`  
  その日の作業内容を書く部分の見出しを指定する。
* `<h_tomorrow>`～`</h_tomorrow>`  
  翌日の作業予定を書く部分の見出しを指定する。
* `<h_other>`～`</h_other>`  
  その他を書く部分の見出しを指定する。
