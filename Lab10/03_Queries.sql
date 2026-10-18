-- ============================================================================
-- File: 03_Queries.sql
-- Mục đích: Thực thi 12 truy vấn dữ liệu và kiểm thử ràng buộc toàn vẹn
-- ============================================================================

USE EduManagerDB;
GO

-- ============================================================================
-- TRUY VẤN 1: Liệt kê toàn bộ sinh viên
-- ============================================================================
PRINT N'--- TRUY VẤN 1: Liệt kê toàn bộ sinh viên ---';
SELECT 
    sv.MaSV AS [Mã Sinh Viên],
    sv.HoTen AS [Họ và Tên],
    CONVERT(VARCHAR(10), sv.NgaySinh, 103) AS [Ngày Sinh],
    sv.GioiTinh AS [Giới Tính],
    sv.MaLop AS [Mã Lớp]
FROM SINHVIEN sv;
GO

-- ============================================================================
-- TRUY VẤN 2: Liệt kê sinh viên kèm tên lớp và tên khoa (INNER JOIN 3 bảng)
-- ============================================================================
PRINT N'--- TRUY VẤN 2: Liệt kê sinh viên kèm tên lớp và tên khoa ---';
SELECT 
    sv.MaSV AS [Mã SV],
    sv.HoTen AS [Họ Tên Sinh Viên],
    sv.GioiTinh AS [Giới Tính],
    l.TenLop AS [Tên Lớp],
    k.TenKhoa AS [Thuộc Khoa]
FROM SINHVIEN sv
INNER JOIN LOP l ON sv.MaLop = l.MaLop
INNER JOIN KHOA k ON l.MaKhoa = k.MaKhoa;
GO

-- ============================================================================
-- TRUY VẤN 3: Tìm sinh viên theo một phần họ tên bằng LIKE
-- ============================================================================
PRINT N'--- TRUY VẤN 3: Tìm sinh viên có chứa chữ "Huy" trong họ tên ---';
SELECT 
    sv.MaSV AS [Mã SV],
    sv.HoTen AS [Họ Tên Sinh Viên],
    sv.GioiTinh AS [Giới Tính],
    sv.MaLop AS [Mã Lớp]
FROM SINHVIEN sv
WHERE sv.HoTen LIKE N'%Huy%';
GO

-- ============================================================================
-- TRUY VẤN 4: Liệt kê sinh viên theo mã lớp (ví dụ lớp: 2311COMP1)
-- ============================================================================
PRINT N'--- TRUY VẤN 4: Liệt kê sinh viên lớp 2311COMP1 ---';
SELECT 
    sv.MaSV AS [Mã SV],
    sv.HoTen AS [Họ Tên],
    sv.GioiTinh AS [Giới Tính],
    CONVERT(VARCHAR(10), sv.NgaySinh, 103) AS [Ngày Sinh],
    sv.MaLop AS [Mã Lớp]
FROM SINHVIEN sv
WHERE sv.MaLop = '2311COMP1';
GO

-- ============================================================================
-- TRUY VẤN 5: Cập nhật họ tên hoặc lớp của một sinh viên
-- ============================================================================
PRINT N'--- TRUY VẤN 5: Cập nhật thông tin sinh viên SV05 ---';
-- Xem trước khi cập nhật
PRINT N'Trước khi cập nhật SV05:';
SELECT MaSV, HoTen, MaLop FROM SINHVIEN WHERE MaSV = 'SV05';

-- Cập nhật họ tên của SV05
UPDATE SINHVIEN
SET HoTen = N'Phạm Gia Khiêm (K49)'
WHERE MaSV = 'SV05';

-- Kiểm tra lại sau cập nhật
PRINT N'Sau khi cập nhật SV05:';
SELECT MaSV, HoTen, MaLop FROM SINHVIEN WHERE MaSV = 'SV05';
GO

-- ============================================================================
-- TRUY VẤN 6: Xóa một sinh viên chưa có dữ liệu điểm (SV07)
-- ============================================================================
PRINT N'--- TRUY VẤN 6: Xóa sinh viên chưa có dữ liệu điểm (SV07) ---';
-- Kiểm tra sinh viên chưa có điểm
PRINT N'Danh sách sinh viên chưa có điểm trước khi xóa:';
SELECT sv.MaSV, sv.HoTen, sv.MaLop 
FROM SINHVIEN sv
WHERE sv.MaSV NOT IN (SELECT DISTINCT MaSV FROM KETQUA);

-- Thực hiện xóa an toàn
DELETE FROM SINHVIEN
WHERE MaSV = 'SV07'
  AND MaSV NOT IN (SELECT DISTINCT MaSV FROM KETQUA);

-- Kiểm tra lại sau khi xóa
PRINT N'Kiểm tra lại sự tồn tại của SV07 sau lệnh DELETE:';
SELECT MaSV, HoTen FROM SINHVIEN WHERE MaSV = 'SV07';
GO

-- ============================================================================
-- TRUY VẤN 7: Liệt kê điểm của từng sinh viên theo từng môn
-- ============================================================================
PRINT N'--- TRUY VẤN 7: Liệt kê điểm của từng sinh viên theo từng môn ---';
SELECT 
    sv.MaSV AS [Mã SV],
    sv.HoTen AS [Họ Tên Sinh Viên],
    mh.MaMH AS [Mã Môn],
    mh.TenMH AS [Tên Môn Học],
    mh.SoTinChi AS [Số Tín Chỉ],
    kq.Diem AS [Điểm]
FROM KETQUA kq
INNER JOIN SINHVIEN sv ON kq.MaSV = sv.MaSV
INNER JOIN MONHOC mh ON kq.MaMH = mh.MaMH
ORDER BY sv.MaSV ASC, mh.MaMH ASC;
GO

-- ============================================================================
-- TRUY VẤN 8: Tính điểm trung bình của từng sinh viên
-- ============================================================================
PRINT N'--- TRUY VẤN 8: Tính điểm trung bình của từng sinh viên ---';
SELECT 
    sv.MaSV AS [Mã SV],
    sv.HoTen AS [Họ Tên Sinh Viên],
    COUNT(kq.MaMH) AS [Số Môn Đã Học],
    CAST(AVG(kq.Diem) AS DECIMAL(4, 2)) AS [Điểm Trung Bình]
FROM SINHVIEN sv
INNER JOIN KETQUA kq ON sv.MaSV = kq.MaSV
GROUP BY sv.MaSV, sv.HoTen
ORDER BY [Điểm Trung Bình] DESC;
GO

-- ============================================================================
-- TRUY VẤN 9: Thống kê số sinh viên theo từng lớp (Dùng LEFT JOIN)
-- ============================================================================
PRINT N'--- TRUY VẤN 9: Thống kê số lượng sinh viên theo từng lớp ---';
SELECT 
    l.MaLop AS [Mã Lớp],
    l.TenLop AS [Tên Lớp],
    k.TenKhoa AS [Khoa],
    COUNT(sv.MaSV) AS [Số Lượng Sinh Viên]
FROM LOP l
INNER JOIN KHOA k ON l.MaKhoa = k.MaKhoa
LEFT JOIN SINHVIEN sv ON l.MaLop = sv.MaLop
GROUP BY l.MaLop, l.TenLop, k.TenKhoa
ORDER BY [Số Lượng Sinh Viên] DESC;
GO

-- ============================================================================
-- TRUY VẤN 10: Liệt kê các lớp có từ 2 sinh viên trở lên (HAVING)
-- ============================================================================
PRINT N'--- TRUY VẤN 10: Liệt kê các lớp có từ 2 sinh viên trở lên ---';
SELECT 
    l.MaLop AS [Mã Lớp],
    l.TenLop AS [Tên Lớp],
    COUNT(sv.MaSV) AS [Số Lượng Sinh Viên]
FROM LOP l
INNER JOIN SINHVIEN sv ON l.MaLop = sv.MaLop
GROUP BY l.MaLop, l.TenLop
HAVING COUNT(sv.MaSV) >= 2
ORDER BY [Số Lượng Sinh Viên] DESC;
GO

-- ============================================================================
-- TRUY VẤN 11: Test lỗi khi thêm sinh viên vào lớp không tồn tại (FK constraint)
-- ============================================================================
PRINT N'--- TRUY VẤN 11: Kiểm thử lỗi thêm sinh viên vào lớp không tồn tại ---';
BEGIN TRY
    INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, MaLop)
    VALUES ('SV99', N'Nguyễn Thất Bại', '2005-01-01', N'Nam', 'LOP_NONE');
    
    PRINT N'[THẤT BẠI] Lỗi không kích hoạt, dữ liệu không hợp lệ đã bị chèn!';
END TRY
BEGIN CATCH
    PRINT N'[THÀNH CÔNG] Đã bắt được vi phạm ràng buộc Khóa Ngoại (Foreign Key)!';
    PRINT N'- Mã lỗi (Error Number): ' + CAST(ERROR_NUMBER() AS NVARCHAR(10));
    PRINT N'- Tên ràng buộc / Chi tiết: ' + ERROR_MESSAGE();
END CATCH;
GO

-- ============================================================================
-- TRUY VẤN 12: Test lỗi khi nhập điểm ngoài khoảng 0 đến 10 (CHECK constraint)
-- ============================================================================
PRINT N'--- TRUY VẤN 12: Kiểm thử lỗi khi nhập điểm ngoài khoảng 0 đến 10 ---';
BEGIN TRY
    -- Thử chèn điểm = 10.5 (Vi phạm ràng buộc CK_KETQUA_Diem)
    INSERT INTO KETQUA (MaSV, MaMH, Diem)
    VALUES ('SV01', 'MATH1001', 10.5);
    
    PRINT N'[THẤT BẠI] Lỗi không kích hoạt, điểm ngoài phạm vi đã bị chèn!';
END TRY
BEGIN CATCH
    PRINT N'[THÀNH CÔNG] Đã bắt được vi phạm ràng buộc CHECK điểm số!';
    PRINT N'- Mã lỗi (Error Number): ' + CAST(ERROR_NUMBER() AS NVARCHAR(10));
    PRINT N'- Tên ràng buộc / Chi tiết: ' + ERROR_MESSAGE();
END CATCH;
GO
