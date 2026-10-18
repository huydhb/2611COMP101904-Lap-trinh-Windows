-- ============================================================================
-- File: 02_InsertSampleData.sql
-- Mục đích: Chèn dữ liệu mẫu vào 6 bảng theo đúng quan hệ
-- Yêu cầu tối thiểu: 2 khoa, 3 lớp, 6 SV, 4 môn học, 8 kết quả, 2 tài khoản
-- ============================================================================

USE EduManagerDB;
GO

-- 1. Nhập dữ liệu bảng KHOA (2 khoa)
INSERT INTO KHOA (MaKhoa, TenKhoa) VALUES
('CNTT', N'Công nghệ Thông tin'),
('TOAN', N'Toán - Tin học');
GO

-- 2. Nhập dữ liệu bảng LOP (3 lớp)
INSERT INTO LOP (MaLop, TenLop, MaKhoa) VALUES
('2311COMP1', N'Lập trình Windows 01', 'CNTT'),
('2311COMP2', N'Lập trình Web 02', 'CNTT'),
('2311MATH1', N'Giải tích ứng dụng 01', 'TOAN');
GO

-- 3. Nhập dữ liệu bảng SINHVIEN (7 sinh viên, trong đó SV07 chưa có điểm để phục vụ test xóa)
INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, MaLop) VALUES
('SV01', N'Đặng Hoàng Bảo Huy', '2005-05-15', N'Nam', '2311COMP1'),
('SV02', N'Nguyễn Thị Kim Ngân', '2005-08-20', N'Nữ',  '2311COMP1'),
('SV03', N'Trần Minh Triết',    '2005-02-10', N'Nam', '2311COMP1'),
('SV04', N'Lê Hoàng Yến',       '2005-11-05', N'Nữ',  '2311COMP2'),
('SV05', N'Phạm Gia Khiêm',     '2005-04-18', N'Nam', '2311COMP2'),
('SV06', N'Võ Quốc An',         '2005-09-22', N'Nam', '2311MATH1'),
('SV07', N'Đỗ Hoài Phương',     '2005-12-30', N'Nữ',  '2311COMP1'); -- Chưa có điểm (phục vụ Query 06)
GO

-- 4. Nhập dữ liệu bảng MONHOC (4 môn học)
INSERT INTO MONHOC (MaMH, TenMH, SoTinChi) VALUES
('COMP1019', N'Lập trình trên Windows', 3),
('COMP1020', N'Lập trình Web',          3),
('COMP1030', N'Cơ sở dữ liệu',          4),
('MATH1001', N'Đại số tuyến tính',      3);
GO

-- 5. Nhập dữ liệu bảng KETQUA (10 kết quả điểm thi)
INSERT INTO KETQUA (MaSV, MaMH, Diem) VALUES
('SV01', 'COMP1019', 9.50),
('SV01', 'COMP1030', 8.75),
('SV01', 'COMP1020', 9.00),
('SV02', 'COMP1019', 8.50),
('SV02', 'COMP1030', 9.00),
('SV03', 'COMP1019', 7.00),
('SV03', 'COMP1020', 6.50),
('SV04', 'COMP1020', 8.00),
('SV04', 'COMP1030', 8.50),
('SV05', 'COMP1019', 6.00),
('SV06', 'MATH1001', 9.00);
GO

-- 6. Nhập dữ liệu bảng TAIKHOAN (3 tài khoản)
INSERT INTO TAIKHOAN (TenDangNhap, MatKhau, VaiTro) VALUES
('admin',      'Admin@123',  N'Admin'),
('gv_quang',   'Gv@2024',    N'Giảng viên'),
('sv_baohuy',  'Huy@2024',   N'Sinh viên');
GO

-- ============================================================================
-- KIỂM TRA SỐ LƯỢNG BẢN GHI ĐÃ NHẬP
-- ============================================================================
SELECT 'KHOA' AS [Bang], COUNT(*) AS [TongSoDong] FROM KHOA
UNION ALL
SELECT 'LOP', COUNT(*) FROM LOP
UNION ALL
SELECT 'SINHVIEN', COUNT(*) FROM SINHVIEN
UNION ALL
SELECT 'MONHOC', COUNT(*) FROM MONHOC
UNION ALL
SELECT 'KETQUA', COUNT(*) FROM KETQUA
UNION ALL
SELECT 'TAIKHOAN', COUNT(*) FROM TAIKHOAN;
GO
