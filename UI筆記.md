Step 1 — 先把 MainForm 設成「MDI Container」

打開 MainForm

在屬性視窗找到：

IsMdiContainer = True

讓 MainForm 可以像 ERP 一樣同時開多個子視窗。

完成後，MainForm 中間會變成灰色區域，就是 MDI Panel。

---

🔹 AllowMinimizeRibbon

控制使用者是否能把 Ribbon 收起來。

True → 可折疊

False → 不能折疊（固定展開）

用途：避免 UI 因為被使用者不小心折起造成誤會。

---

🔹 RibbonStyle

設定 Ribbon 的外觀風格（像套用主題）。

常用：

OfficeUniversal → 現代扁平風

Office2019 → 微軟 Office 風格

用途：讓你的系統 UI 看起來統一化、專業化。

---

🔹 ShowExpandCollapseButton

決定是否顯示右上角「折疊/展開」箭頭。

True → 顯示按鈕

False → 隱藏按鈕

用途：如果你不想讓使用者亂折疊 Ribbon，就把它關掉。