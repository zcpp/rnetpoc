# 样本数据
inputfile <- "Data/TestData_glm.csv"
testdata <- read.csv(inputfile, header=TRUE,encoding="UTF-8")
#print(testdata )
am <- testdata$am
cyl <- testdata$cyl
hp <- testdata$hp
wt <- testdata$wt

# 提交给 glm() 函数
relation = glm(formula = am ~ cyl + hp + wt, data =testdata, family = binomial)
result <- summary(relation)
print(result)