# 样本数据
inputfile <- "Data/TestData_lm.csv"
testdata <- read.csv(inputfile, encoding="UTF-8")
print(testdata)
Weight <- testdata$Weight
Height <- testdata$Height

# 提交给 lm() 函数
relation <- lm(Weight~Height)

# 判断身高为 170cm 的体重
a <- data.frame(Height= 170)
result <-  predict(relation,a)
print(result)

# 生存 png 图片
output_png <- "lmoutput.png"
png(file =output_png)

# 生成图表
plot(Weight,Height,col = "blue",main = "Height & Weight Regression", abline(lm(Weight~Height)),cex = 1.3,pch = 16,xlab = "Weight in Kg",ylab = "Height in cm")