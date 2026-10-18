-- ============================================================================
-- File: 01_CreateDatabase.sql
-- Mục đích: Tạo cơ sở dữ liệu, các bảng, khóa chính, khóa ngoại và ràng buộc CHECK
-- ============================================================================

USE master;
GO

-- 1. Xóa Database nếu đã tồn tại (Đóng tất cả kết nối đang mở)
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'EduManagerDB')
BEGIN
    ALTER DATABASE EduManagerDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE EduManagerDB;
    PRINT N'Đã xóa cơ sở dữ liệu EduManagerDB cũ thành công.';
END
GO

-- 2. Tạo Database EduManagerDB
CREATE DATABASE EduManagerDB;
GO

PRINT N'Đã khởi tạo cơ sở dữ liệu EduManagerDB thành công.';
GO

-- 3. Chuyển ngữ cảnh sang EduManagerDB
USE EduManagerDB;
GO

-- ============================================================================
-- TẠO CÁC BẢNG THEO THỨ TỰ THAM CHIẾU (CHA TRƯỚC, CON SAU)
-- ============================================================================

-- Bảng 1: KHOA
CREATE TABLE KHOA (
    MaKhoa VARCHAR(10) NOT NULL,
    TenKhoa NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_KHOA PRIMARY KEY (MaKhoa)
);
GO

-- Bảng 2: LOP (Thuộc một Khoa)
CREATE TABLE LOP (
    MaLop VARCHAR(10) NOT NULL,
    TenLop NVARCHAR(100) NOT NULL,
    MaKhoa VARCHAR(10) NOT NULL,
    CONSTRAINT PK_LOP PRIMARY KEY (MaLop),
    CONSTRAINT FK_LOP_KHOA FOREIGN KEY (MaKhoa) REFERENCES KHOA(MaKhoa)
);
GO

-- Bảng 3: SINHVIEN (Thuộc một Lớp, Giới tính chỉ nhận Nam hoặc Nữ)
CREATE TABLE SINHVIEN (
    MaSV VARCHAR(10) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NULL,
    GioiTinh NVARCHAR(5) NOT NULL,
    MaLop VARCHAR(10) NOT NULL,
    CONSTRAINT PK_SINHVIEN PRIMARY KEY (MaSV),
    CONSTRAINT FK_SINHVIEN_LOP FOREIGN KEY (MaLop) REFERENCES LOP(MaLop),
    CONSTRAINT CK_SINHVIEN_GioiTinh CHECK (GioiTinh IN (N'Nam', N'Nữ'))
);
GO

-- Bảng 4: MONHOC (Số tín chỉ > 0)
CREATE TABLE MONHOC (
    MaMH VARCHAR(10) NOT NULL,
    TenMH NVARCHAR(100) NOT NULL,
    SoTinChi INT NOT NULL,
    CONSTRAINT PK_MONHOC PRIMARY KEY (MaMH),
    CONSTRAINT CK_MONHOC_SoTinChi CHECK (SoTinChi > 0)
);
GO

-- Bảng 5: KETQUA (Lưu điểm của sinh viên theo môn, Điểm từ 0 đến 10)
CREATE TABLE KETQUA (
    MaSV VARCHAR(10) NOT NULL,
    MaMH VARCHAR(10) NOT NULL,
    Diem DECIMAL(4, 2) NOT NULL,
    CONSTRAINT PK_KETQUA PRIMARY KEY (MaSV, MaMH),
    CONSTRAINT FK_KETQUA_SINHVIEN FOREIGN KEY (MaSV) REFERENCES SINHVIEN(MaSV),
    CONSTRAINT FK_KETQUA_MONHOC FOREIGN KEY (MaMH) REFERENCES MONHOC(MaMH),
    CONSTRAINT CK_KETQUA_Diem CHECK (Diem >= 0.0 AND Diem <= 10.0)
);
GO

-- Bảng 6: TAIKHOAN (Lưu tài khoản đăng nhập demo)
CREATE TABLE TAIKHOAN (
    TenDangNhap VARCHAR(50) NOT NULL,
    MatKhau VARCHAR(255) NOT NULL,
    VaiTro NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_TAIKHOAN PRIMARY KEY (TenDangNhap),
    CONSTRAINT CK_TAIKHOAN_VaiTro CHECK (VaiTro IN (N'Admin', N'Giảng viên', N'Sinh viên'))
);
GO

-- ============================================================================
-- KIỂM TRA THÔNG TIN SCHEMA ĐÃ TẠO
-- ============================================================================
PRINT N'Danh sách các bảng đã được khởi tạo:';
SELECT 
    t.name AS [TenBang],
    c.name AS [CotKhoaChinh]
FROM sys.tables t
LEFT JOIN sys.key_constraints kc ON t.object_id = kc.parent_object_id AND kc.type = 'PK'
LEFT JOIN sys.index_columns ic ON kc.parent_object_id = ic.object_id AND kc.unique_index_id = ic.index_id
LEFT JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
ORDER BY t.name;
GO
