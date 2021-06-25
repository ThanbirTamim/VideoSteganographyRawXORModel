%PSNR, SNR, MSE, RMSE, MAE, SSIM measurement
clear
clc
close all
cover = imread('C:/VideoSteganography/allFrames/000003.bmp');
%tstego = imread('C:/VideoSteganography/stegoFrameStore/000003.bmp');
tstego = imread('C:/VideoSteganography/allFramesStego/000003.bmp');

[peaksnr, snr] = psnr(tstego, cover);
im1 = double(cover);
im2 = double(tstego);
mse = sum((im1(:)-im2(:)).^2) / prod(size(im1));
psnr = 10*log10((255*255)/mse);
rmse = sqrt(mse);
absDiff = abs(double(im1) - double(im2));
mae = mean(absDiff(:));
SSIM = ssim(im1, im2);
%absulateDiff = imabsdiff(im1,im2);
%imshow(absulateDiff,[]) %difference dekhar jonno
fprintf('\nThis for existing Model: ');
fprintf('\n     The MSE value is %0.4f', mse);
fprintf('\n     The RMSE value is %0.4f', rmse);
fprintf('\n     The SNR value is %0.4f ', snr);
fprintf('\n     The MAE value is %0.4f', mae);
fprintf('\n     The SSIM value is %0.9f', SSIM);
fprintf('\n     The Peak-SNR value is %0.4f \n', peaksnr);

